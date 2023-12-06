using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Startup.DependencyInjectionSetup;
using System.Text.Json.Serialization;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.helper.Data;
using sso.mms.fees.api.Configs;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/* IDP CONNECTION */
var Environment = builder.Configuration.GetSection("Environment").Value;
var ConnectionStrings = builder.Configuration.GetSection($"ConnectionStrings_{Environment}").Get<ConnectionStringModels>();
builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql($"{ConnectionStrings.IdpDbContext}"));

builder.Services.RegisterPromoteHealthServices();
builder.Services.RegisterEdocEsigServices();
builder.Services.RegisterDentalServices();
builder.Configuration.PromoteHealthConfigurationBuilder();
builder.Configuration.DentalConfigurationBuilder();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
