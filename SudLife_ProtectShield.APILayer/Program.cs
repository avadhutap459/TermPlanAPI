using Microsoft.AspNetCore.Mvc;
using NLog;
using SudLife_ProtectShield.APILayer.API.Database;
using SudLife_ProtectShield.APILayer.API.Global.FException;
using SudLife_ProtectShield.APILayer.API.Global.Filter;
using SudLife_ProtectShield.APILayer.API.Service.Common;
using SudLife_ProtectShield.APILayer.API.Service.DynamicParams;
using SudLife_ProtectShield.APILayer.API.Service.ProtectShield;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(string.Concat(System.Environment.CurrentDirectory, "/nlog.config"));

LogManager.Configuration.Variables["mydir"] = string.Concat(System.Environment.CurrentDirectory, "/Logger");

builder.Services.AddControllers();
builder.Services.AddTransient<IGenericRepo, GenericRepo>();
builder.Services.AddTransient<CommonOperations>();
builder.Services.AddTransient<DynamicCollections>();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddTransient<IProtectShieldSvc,ProtectShieldSvc>();
builder.Services.AddScoped<ValidationResultFilter>();

builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    Options.SuppressModelStateInvalidFilter = true;


});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.CongigureExceptionMiddleware();

app.MapControllers();

app.Run();
