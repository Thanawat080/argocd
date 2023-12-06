using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using MatBlazor;
using sso.mms.notification.Services;
using sso.mms.login.Services;
using sso.mms.news.Services;
using sso.mms.chat.Services;
using sso.mms.helper.Services;
using sso.mms.portal.admin.Services;
using sso.mms.portal.ext.Services;
using sso.mms.helper.PortalModel;
using sso.mms.login.Interface;
using CurrieTechnologies.Razor.SweetAlert2;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});

builder.Services.AddHttpClient("insecure").ConfigurePrimaryHttpMessageHandler(() => {
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));
builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql(ConfigureCore.idPConnectionString));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddHttpClient();

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient client = new HttpClient(clientHandler);

builder.Services.AddControllersWithViews();
builder.Services.AddAntDesign();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddSingleton<NewsService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddScoped<IKeyCloakService, KeyCloakService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<UploadFileService>();
builder.Services.AddSingleton<BannerService>();
builder.Services.AddSingleton<CertificateService>();
builder.Services.AddSingleton<SettingOpendataService>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSweetAlert2();
builder.Services.AddSingleton<ManageMentMenuService>();
builder.Services.AddSingleton<AddNewUserService>();
builder.Services.AddSingleton<AnnounceService>();
builder.Services.AddSingleton<SettingService>();

builder.Services.AddHttpClient<SettingService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});
builder.Services.AddHttpClient<AnnounceService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<AddNewUserService>(client =>
{

    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalExt);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<ManageMentMenuService>(client =>
{


    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<CertificateService>(client =>
{

    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalExt);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<SettingOpendataService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<BannerService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<ChatRoomService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressChat);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<ChatService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressChat);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<NotificationService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressNotification);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddHttpClient<NewsService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressNews);
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
builder.Services.AddHttpClient<KeyCloakService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressIdpKeycloak);
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

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllers();

app.UseHttpsRedirection();


app.UseCookiePolicy();


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
