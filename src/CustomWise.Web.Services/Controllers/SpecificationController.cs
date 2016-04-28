﻿using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Base;
using DtoEntities = CustomWise.Web.Services.Models;
using DalEntities = CustomWise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/recordtype")]
    public class SpecificationController
        : BaseController {
        private ICustomWiseContext _context;
       
        public SpecificationController()
            : base(AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperInstance()) {
            _context = new CustomWiseModel();
        }

        public SpecificationController(ICustomWiseContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(autoMapperConfigProvier, mapper) {
            _context = context;
        }

        [Route]
        public async Task<IEnumerable<DtoEntities.Specification>> Get() {
            return await _context.Specifications.ProjectToListAsync<DtoEntities.Specification>(AutoMapperConfigProvider);
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            return await _context.Specifications
                                 .Where(s => s.Id == id)
                                 .ProjectToSingleAsync<DtoEntities.Specification>(AutoMapperConfigProvider);
        }

        [Route]
        public async Task<int> Put(DtoEntities.Specification specification, bool published = false) {
            return (await Put(new List<DtoEntities.Specification> { specification }, published))[0];
        }

        [Route]
        public async Task<int[]> Put(IEnumerable<DtoEntities.Specification> specifications, bool published = false) {
            var specToSaveList = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specifications);

            // get version
            var specVersion = _context.SpecificationVersions.Find(specifications.First().SpecificationVersionId);
            var flattenSpecificationList = specToSaveList.Flatten(e => e.SubSpecifications).ToList();

            if (specVersion.Published) {
                specVersion = await CreateNewVersion(specVersion.Name);
                flattenSpecificationList.ForEach(spec => {
                    spec.Id = 0;
                    spec.SpecificationVersionId = specVersion.Id;
                    spec.SpecificationVersion = specVersion;
                });
                _context.Specifications.AddRange(flattenSpecificationList);
            } else {
                specVersion.Published = published;
                specVersion.PublishedDate = published ? DateTime.Now : specVersion.PublishedDate;

                var updates = flattenSpecificationList.ToDictionary(spec => spec.Id);
                var inserts = flattenSpecificationList.Where(spec => specVersion.Specifications.All(s => s.Id != spec.Id));
                var deletes = specVersion.Specifications.Where(spec => flattenSpecificationList.All(s => s.Id != spec.Id)).Select(spec => spec.Id);

                specVersion.Specifications.Where(spec => updates.ContainsKey(spec.Id))
                    .ToList()
                    .ForEach(spec => {
                        spec.Order = updates[spec.Id].Order;
                        spec.ParentSpecificationId = updates[spec.Id].ParentSpecificationId;
                        spec.DisplayName = updates[spec.Id].DisplayName;
                        spec.IsActive = updates[spec.Id].IsActive;
                        spec.Deleted = deletes.Contains(spec.Id);
                        spec.SpecificationTypeId = updates[spec.Id].SpecificationTypeId;
                    });
                _context.Specifications.AddRange(inserts);
            }

            await _context.SaveChangesAsync();
            return specToSaveList.Select(s => s.Id).ToArray();
        }


        internal async Task<DalEntities.SpecificationVersion> CreateNewVersion(string oldName) {
            var newVersion = new DalEntities.SpecificationVersion {
                Name = "Created from : " + oldName,
                Published = false,
                PublishedDate = null
            };
            _context.SpecificationVersions.Add(newVersion);
            await _context.SaveChangesAsync();

            return newVersion;
        }
    }

    /// <summary>
    /// Custom extensions
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// Get dictionary by key, if key does not exist return default instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns item at the provided key.</returns>
        public static U ByKey<T, U>(this IDictionary<T, U> dictionary, T key) {
            return dictionary == null ? default(U)
                : dictionary.ContainsKey(key) ? dictionary[key]
                : default(U);
        }

        /// <summary>
        /// Flattens a generic type  and it's children of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="getSubItems">The function to get the sub items.</param>
        /// <param name="initialList">The initial list.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> Flatten<T>(this T item, Func<T, IEnumerable<T>> getSubItems, IList<T> initialList = null) {
            initialList = initialList ?? new List<T>();

            if (initialList.Contains(item)) {
                return initialList;
            }

            initialList.Add(item);

            foreach (var subItem in getSubItems(item)) {
                subItem.Flatten(getSubItems, initialList);
            }

            return initialList;
        }

        /// <summary>
        /// Flattens an <see cref="IEnumerable{T}"/> and returns an <see cref="IEnumerable{T}"/> back.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items to flatten.</param>
        /// <param name="getSubItems">The function to get the sub items.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> getSubItems) {
            return items.SelectMany(item => item.Flatten(getSubItems));
        }

        /// <summary>
        /// Check as list to see if an item exist In the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="testList">The test list.</param>
        /// <returns>Returns <c>true</c> if item was found in list; else <c>false</c></returns>
        public static bool In<T>(this T item, params T[] testList) {
            if (testList == null)
                return false;

            return testList.Contains(item);
        }
    }
}
