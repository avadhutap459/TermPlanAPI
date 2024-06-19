namespace SudLife_MasterField.APILayer.API.Global.Dependancy
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
