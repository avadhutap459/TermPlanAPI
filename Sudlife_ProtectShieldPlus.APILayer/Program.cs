using NLog;
using Sudlife_ProtectShieldPlus.APILayer.API.Database;
using Sudlife_ProtectShieldPlus.APILayer.API.Global.FException;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.Common;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.DynamicParams;
using Sudlife_ProtectShieldPlus.APILayer.API.Service.ProtectShieldPlus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(string.Concat(System.Environment.CurrentDirectory, "/nlog.config"));

LogManager.Configuration.Variables["mydir"] = string.Concat(System.Environment.CurrentDirectory, "/Logger");

builder.Services.AddControllers();

builder.Services.AddTransient<IProtectShieldPlusSvc, ProtectShieldPlusSvc>();
builder.Services.AddTransient<IGenericRepo, GenericRepo>();

builder.Services.AddTransient<CommonOperations>();
builder.Services.AddTransient<DynamicCollections>();
builder.Services.AddTransient<ExceptionMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.CongigureExceptionMiddleware();

app.MapControllers();

app.Run();
