using SudLife_ProtectShield.APILayer.API.Database;
using SudLife_ProtectShield.APILayer.API.Global.FException;
using SudLife_ProtectShield.APILayer.API.Service.Common;
using SudLife_ProtectShield.APILayer.API.Service.DynamicParams;
using SudLife_ProtectShield.APILayer.API.Service.ProtectShield;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IGenericRepo, GenericRepo>();
builder.Services.AddTransient<CommonOperations>();
builder.Services.AddTransient<DynamicCollections>();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddTransient<IProtectShieldSvc,ProtectShieldSvc>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.CongigureExceptionMiddleware();

app.MapControllers();

app.Run();
