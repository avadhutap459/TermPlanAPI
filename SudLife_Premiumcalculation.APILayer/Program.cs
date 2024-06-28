global using SecurityMechansim.ServiceLayer.Interface;
global using SudLife_Premiumcalculation.APILayer.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using SudLife_Premiumcalculation.APILayer.API.Global.Dependancy;
using SudLife_Premiumcalculation.APILayer.API.Global.FException;
using SudLife_Premiumcalculation.APILayer.API.Global.Filter;

var config = new ConfigurationBuilder().SetBasePath(System.Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
string env = config.GetSection("Env").Value;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.DependancyInjection(builder.Configuration);


builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    Options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();

app.UseAuthorization();

//app.Use(next => context => {
//    context.Request.EnableBuffering();
//    return next(context);
//});

app.MapControllers();

app.Run();
