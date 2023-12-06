using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.PortalModel;
using sso.mms.chat.Services;
using sso.mms.helper.Configs;
using Blazored.LocalStorage;
using sso.mms.login.Services;
using sso.mms.helper.Services;
using sso.mms.login.Interface;
using sso.mms.notification.Services;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient client = new HttpClient(clientHandler);

builder.Services.AddServerSideBlazor();
builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<ChatRoomService>();
builder.Services.AddSingleton<UploadFileService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddHttpClient<NotificationService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressNotification);
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

builder.Services.AddHttpClient<ReadTokenService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
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


builder.Services.AddHttpClient<UserRoleService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "../sso.mms.helper/wwwroot")),
    RequestPath = "/helper_shared"
});

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
