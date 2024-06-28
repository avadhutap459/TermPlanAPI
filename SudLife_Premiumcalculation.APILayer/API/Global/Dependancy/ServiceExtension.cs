using SecurityMechansim.ServiceLayer.Service;
using SudLife_Premiumcalculation.APILayer.API.Global.Filter;
using SudLife_Premiumcalculation.APILayer.API.Service.Services;

namespace SudLife_Premiumcalculation.APILayer.API.Global.Dependancy
{
    public static class ServiceExtension
    {
        public static IServiceCollection DependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IChecksum, ClsChecksum>();
            services.AddScoped<IDigitalSign, ClsDigitalSigning>();
            services.AddScoped<IEncryptionNDecryption, ClsEncyptionDecryption>();
            services.AddScoped<ITokengeneration, ClsTokenGeneration>();
            services.AddScoped<ISource, ClsSourcesvc>();
            services.AddScoped<ClsCustomManipulationFilter>();
            return services;
        }
    }
}
