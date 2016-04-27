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
            CreateMap<DalEntities.MetaDataDefinition, DtoEntities.MetaDataDefinition>();
            CreateMap<DalEntities.MetaDataDefinitionDetail, DtoEntities.MetaDataDefinitionDetail>();
            CreateMap<DalEntities.SpecificationType, DtoEntities.SpecificationType>();
            CreateMap<DalEntities.Specification, DtoEntities.Specification>();
            CreateMap<DalEntities.SpecificationVersion, DtoEntities.SpecificationVersion>();
        }
    }
}