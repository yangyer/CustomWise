using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomWise.Web.Services.Extensions {
    public static class AutoMapperExtensions {
        /// <summary>
        ///   Convenience method to resolve a member using the 
        ///   <see cref="IMappingOperationOptions.Items"/> dictionary,
        ///   with the name of the member as the key.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="map"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination>
          ResolveFromItems<TSource, TDestination, TProperty>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, TProperty>> selector) {
            var expression = selector.Body as MemberExpression;
            Debug.Assert(expression != null);
            var key = expression.Member.Name;

            return map.ForMember(key, opt => opt.FromItems(key));
        }

        /// <summary>
        ///   Convenience method to resolve a member using the 
        ///   <see cref="IMappingOperationOptions.Items"/> dictionary,
        ///   with a custom key name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   key is null or empty.
        /// </exception>
        public static IMemberConfigurationExpression<T>
          FromItems<T>(this IMemberConfigurationExpression<T> opt,
            string key) {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Argument is null or empty", nameof(key));

            opt.ResolveUsing(res => res.Context.Options.Items[key]);
            return opt;
        }

        /// <summary>
        ///   Convenience method to insert an item into the 
        ///   <see cref="IMappingOperationOptions.Items"/> dictionary,
        ///   using the name of the selected member as the key.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="opt"></param>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        public static void ItemFor<TSource, TDestination, TProperty>(
          this IMappingOperationOptions<TSource, TDestination> opt,
          Expression<Func<TDestination, TProperty>> selector,
          object value) {
            var expression = selector.Body as MemberExpression;
            Debug.Assert(expression != null);
            var key = expression.Member.Name;

            opt.Items[key] = value;
        }
    }
}
