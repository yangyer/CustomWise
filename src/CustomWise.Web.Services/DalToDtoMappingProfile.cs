namespace CustomWise.Web.Services {
    using AutoMapper;
    using DalEntities = Data.Entities;
    using DtoEntities = Models;
    using System.Linq;
    using Extensions;
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
            CreateMap<DalEntities.SpecificationVersion, DalEntities.Specification>()
                .ForMember(m => m.Id, opts => opts.MapFrom(s => s.SpecificationId))
                .ReverseMap()
                .ForMember(m => m.Id, opts => opts.Ignore())
                .ForMember(m => m.SpecificationId, opts => opts.MapFrom(s => s.Id));
            CreateMap<DalEntities.MetaDataVersion, DalEntities.MetaData>()
                .ForMember(m => m.Id, opts => opts.MapFrom(s => s.MetaDataId))
                .ReverseMap()
                .ForMember(m => m.Id, opts => opts.Ignore())
                .ForMember(m => m.MetaDataId, opts => opts.MapFrom(s => s.Id));
            CreateMap<DalEntities.ArtifactVersion, DalEntities.Artifact>()
                .ForMember(m => m.Id, opts => opts.MapFrom(s => s.ArtifactId))
                .ReverseMap()
                .ForMember(m => m.Id, opts => opts.Ignore())
                .ForMember(m => m.ArtifactId, opts => opts.MapFrom(s => s.Id));

            CreateMap<DalEntities.Artifact, DtoEntities.Specification>()
                .ForMember(m => m.Configurations, opts => opts.Ignore());
            CreateMap<DalEntities.ArtifactType, DtoEntities.SpecificationType>();
        }
    }
}