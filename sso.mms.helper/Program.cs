using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;

var builder = WebApplication.CreateBuilder(args);

var ConfigENV = ConfigureCore.ConfigENV;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql(ConfigureCore.idPConnectionString));

builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
};


app.Environment.IsDevelopment();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapControllers();

app.MapRazorPages();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
