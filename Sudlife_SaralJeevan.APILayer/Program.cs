using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog;
using Sudlife_SaralJeevan.APILayer.API.Database;
using Sudlife_SaralJeevan.APILayer.API.Global.FException;
using Sudlife_SaralJeevan.APILayer.API.Global.Filter;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Sudlife_SaralJeevan.APILayer.API.Service.Common;
using Sudlife_SaralJeevan.APILayer.API.Service.DynamicParams;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(System.Environment.CurrentDirectory, "/nlog.config"));

LogManager.Configuration.Variables["mydir"] = string.Concat(System.Environment.CurrentDirectory, "/Logger");

// Add services to the container.
builder.Services.AddTransient<CommonOperations>();
builder.Services.AddTransient<DynamicCollections>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<DigitallySignedResponse>();
builder.Services.AddTransient<IGenericRepo,GenericRepo>();
builder.Services.AddTransient<IJWTService, JWTService>();
builder.Services.AddTransient<ISaralJeevanSvc, SaralJeevanSvc>();

builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddScoped<DigitalSignFilter>();
builder.Services.AddScoped<JWTValidationFilter>();

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    Options.SuppressModelStateInvalidFilter = true;


});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();

app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
