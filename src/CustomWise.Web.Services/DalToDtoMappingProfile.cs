namespace CustomWise.Web.Services {
    using AutoMapper;
    using DalEntities = Data.Entities;
    using DtoEntities = Models;
    using System.Linq;

    internal class DalToDtoMappingProfile
        : Profile {
        public override string ProfileName { get; } = "DalToDtoMappingProfile";

        protected override void Configure() {
            CreateMap<DalEntities.Configuration, DtoEntities.Configuration>().ReverseMap();
            CreateMap<DalEntities.MetaData, DtoEntities.MetaData>().ReverseMap();
            CreateMap<DalEntities.MetaDataDefinition, DtoEntities.MetaDataDefinition>().ReverseMap();
            CreateMap<DalEntities.MetaDataDefinitionDetail, DtoEntities.MetaDataDefinitionDetail>().ReverseMap();
            CreateMap<DalEntities.SpecificationType, DtoEntities.SpecificationType>().ReverseMap();
            CreateMap<DalEntities.Specification, DtoEntities.Specification>()
                .ForMember(m => m.ReferenceId, opts => opts.MapFrom(s => s.ArtifactReferenceId))
                .ReverseMap();
            CreateMap<DalEntities.SpecificationVersion, DalEntities.Specification>().ReverseMap();
        }
    }
}