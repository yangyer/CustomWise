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
                .ForMember(m => m.ReferenceID, opts => opts.MapFrom(s => s.ArtifactReferenceID))
                .ReverseMap();
            //CreateMap<DalEntities.SpecificationVersion, DalEntities.Specification>()
            //    .ForMember(m => m.ID, opts => opts.MapFrom(s => s.SpecificationID))
            //    .ReverseMap()
            //    .ForMember(m => m.ID, opts => opts.Ignore())
            //    .ForMember(m => m.SpecificationID, opts => opts.MapFrom(s => s.ID));
            //CreateMap<DalEntities.MetaDataVersion, DalEntities.MetaData>()
            //    .ForMember(m => m.ID, opts => opts.MapFrom(s => s.MetaDataID))
            //    .ReverseMap()
            //    .ForMember(m => m.ID, opts => opts.Ignore())
            //    .ForMember(m => m.MetaDataID, opts => opts.MapFrom(s => s.ID));
            //CreateMap<DalEntities.ArtifactVersion, DalEntities.Artifact>()
            //    .ForMember(m => m.ID, opts => opts.MapFrom(s => s.ArtifactID))
            //    .ReverseMap()
            //    .ForMember(m => m.ID, opts => opts.Ignore())
            //    .ForMember(m => m.ArtifactID, opts => opts.MapFrom(s => s.ID));

            CreateMap<DalEntities.Artifact, DtoEntities.Specification>()
                .ForMember(m => m.Configurations, opts => opts.Ignore());
            CreateMap<DalEntities.ArtifactType, DtoEntities.SpecificationType>();
        }
    }
}