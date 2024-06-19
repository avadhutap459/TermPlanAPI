using SudLife_Premiumcalculation.APILayer.API.Global.Dependancy;
using SudLife_Premiumcalculation.APILayer.API.Global.FException;

var config = new ConfigurationBuilder().SetBasePath(System.Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
string env = config.GetSection("Env").Value;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.DependancyInjection(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
