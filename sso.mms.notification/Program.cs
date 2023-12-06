using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.login.Services;
using sso.mms.notification.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddSingleton<UserRoleService>();

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
HttpClient client = new HttpClient(clientHandler);


builder.Services.AddHttpClient<NotificationService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressNotification);
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
builder.Services.AddHttpClient<ReadTokenService>(client =>
{
    client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});

builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

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
