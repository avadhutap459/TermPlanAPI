using Sudlife_SaralJeevan.APILayer.API.Database;
using Sudlife_SaralJeevan.APILayer.API.Global.FException;
using Sudlife_SaralJeevan.APILayer.API.Service;
using Sudlife_SaralJeevan.APILayer.API.Service.DynamicParams;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CommonOperation>();
builder.Services.AddTransient<DynamicCollections>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<IGenericRepo,GenericRepo>();
builder.Services.AddTransient<ISaralJeevanSvc, SaralJeevanSvc>();// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
