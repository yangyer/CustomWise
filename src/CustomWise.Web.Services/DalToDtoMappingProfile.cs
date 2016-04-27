namespace CustomWise.Web.Services {
    using AutoMapper;
    using DalEntities = Data.Entities;
    using DtoEntities = Models;
    using System.Linq;

    internal class DalToDtoMappingProfile
        : Profile {
        public override string ProfileName { get; } = "DalToDtoMappingProfile";

        protected override void Configure() {
            CreateMap<DalEntities.Configuration, DtoEntities.Configuration>();
            CreateMap<DalEntities.MetaData, DtoEntities.MetaData>();
            CreateMap<DalEntities.MetaDataType, DtoEntities.MetaDataType>();
            CreateMap<DalEntities.SpecificationType, DtoEntities.RecordType>();
            CreateMap<DalEntities.Specification, DtoEntities.Specification>();
            CreateMap<DalEntities.SpecificationLocal, DtoEntities.SpecificationLocal>();
            CreateMap<DalEntities.SpecificationVersion, DtoEntities.SpecificationVersion>();
        }
    }
}