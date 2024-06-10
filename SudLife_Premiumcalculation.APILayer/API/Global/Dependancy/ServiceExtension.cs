using System.Collections.Generic;

namespace SudLife_Premiumcalculation.APILayer.API.Global.Dependancy
{
    public static class ServiceExtension
    {
        public static IServiceCollection DependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IEncryptDecryptService, ClsEncryptDecryptService>();
            return services;
        }
    }
}
