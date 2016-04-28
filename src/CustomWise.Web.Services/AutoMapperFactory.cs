namespace CustomWise.Web.Services {
    using AutoMapper;

    public class AutoMapperFactory {
        #warning: Temp until DI is implemented.
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;

        public static MapperConfiguration CreateAutoMapperConfigProviderInstance() => _mapperConfig != null ? _mapperConfig : _mapperConfig = new MapperConfiguration(config => config.AddProfile<DalToDtoMappingProfile>());

        public static IMapper CreateAutoMapperInstance() => _mapper != null ? _mapper : _mapper = CreateAutoMapperConfigProviderInstance().CreateMapper();
    }
}
