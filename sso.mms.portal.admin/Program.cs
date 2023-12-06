using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sso.mms.chat.Services;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.login.Interface;
using sso.mms.login.Services;
using sso.mms.news.Services;
using sso.mms.notification.Services;
using sso.mms.portal.admin.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpClient();

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient client = new HttpClient(clientHandler);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql(ConfigureCore.idPConnectionString));
builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));

builder.Services.AddScoped<IKeyCloakService, KeyCloakService>();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<NewsService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<BannerService>();
builder.Services.AddSingleton<UploadFileService>();
builder.Services.AddSingleton<SettingOpendataService>();
builder.Services.AddSingleton<AdminService>();
builder.Services.AddSingleton<ManageMentMenuService>();
builder.Services.AddSingleton<SettingMService>();
builder.Services.AddSingleton<AuditSevices>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSingleton<AnnounceService>();
builder.Services.AddSingleton<SettingService>();
//builder.Services.AddSingleton<ChatService>();
//builder.Services.AddSingleton<ChatRoomService>();

builder.Services.AddSweetAlert2();
builder.Services.AddAntDesign();



try
{
    builder.Services.AddHttpClient<SettingService>(client =>
    {

        client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
    }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

    });
    builder.Services.AddHttpClient<AdminService>(client =>
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

    builder.Services.AddHttpClient<AuditSevices>(client =>
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

    builder.Services.AddHttpClient<ManageMentMenuService>(client =>
    {

        client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
    }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

    });

    builder.Services.AddHttpClient<SettingMService>(client =>
    {

        client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
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

    builder.Services.AddHttpClient<KeyCloakService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressIdpKeycloak);
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

    builder.Services.AddHttpClient<ReadTokenService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
    }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

    });
}
catch (Exception ex)
{

}

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "../sso.mms.helper/wwwroot")),
    RequestPath = "/helper_shared"
});


app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
