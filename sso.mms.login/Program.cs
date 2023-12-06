using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.login.Interface;
using sso.mms.login.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using MatBlazor;
using sso.mms.helper.PortalModel;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMatBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSweetAlert2();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient client = new HttpClient(clientHandler);

builder.Services.AddAntDesign(); 
builder.Services.AddScoped<IKeyCloakService, KeyCloakService>();
builder.Services.AddScoped<IUtilService, UtilService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddSingleton<MasterProvinceService>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSingleton<SettingService>();
builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql(ConfigureCore.idPConnectionString));
builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddHttpClient<KeyCloakService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressIdpKeycloak);
   
}).ConfigurePrimaryHttpMessageHandler(_=> new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
});

builder.Services.AddHttpClient<MasterProvinceService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);

}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<ReadTokenService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<SettingService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);

}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
});

builder.Services.AddHttpClient<UserRoleService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<UserService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllers();

app.MapRazorPages();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "../sso.mms.helper/wwwroot")),
    RequestPath = "/helper_shared"
});


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
