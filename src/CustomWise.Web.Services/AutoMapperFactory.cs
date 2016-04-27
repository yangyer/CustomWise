namespace CustomWise.Web.Services {
    using AutoMapper;

    public class AutoMapperFactory {
        public static MapperConfiguration CreateAutoMapperConfigProviderInstance() {
            return new MapperConfiguration((config) => config.AddProfile<DalToDtoMappingProfile>());
        }

        public static IMapper CreateAutoMapperMapperInstance() {
            return new MapperConfiguration((config) => config.AddProfile<DalToDtoMappingProfile>()).CreateMapper();
        }
    }
}
