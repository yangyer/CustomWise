namespace CustomWise.Web.Services {
    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;

    public class AutoMapperFactory {
        #warning: Temp until DI is implemented.
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;

        public static MapperConfiguration CreateAutoMapperConfigProviderInstance() => _mapperConfig ?? new MapperConfiguration(config => config.AddProfile<DalToDtoMappingProfile>());

        public static IMapper CreateAutoMapperMapperInstance() => _mapper ?? CreateAutoMapperConfigProviderInstance().CreateMapper();
    }
}
