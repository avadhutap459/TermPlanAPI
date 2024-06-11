using SecurityMechansim.ServiceLayer.Interface;
using SecurityMechansim.ServiceLayer.Service;

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
            return services;
        }
    }
}
