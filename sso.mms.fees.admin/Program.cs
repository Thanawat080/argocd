using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using sso.mms.helper.Components.Navbar;
using sso.mms.login.Services;
using sso.mms.login.Interface;
using AntDesign;
using sso.mms.notification.Services;
using Radzen;
using NotificationService = sso.mms.notification.Services.NotificationService;
using MatBlazor;
using sso.mms.fees.admin.Data;
using sso.mms.fees.admin.Providers.PromoteHealth.AssignHealthChecklist;
using sso.mms.fees.admin.Providers.PromoteHealth.PaymentOrderList;
using sso.mms.fees.admin.Providers.PromoteHealth.DisbursementHistoryPromoteList;
using sso.mms.fees.admin.Providers.PromoteHealth.ReportsPromoteAIList;
using sso.mms.fees.admin.Providers.Dental.RecordCar;
using sso.mms.fees.admin.Providers.VerifyAdminIdentify;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddDbContext<IdpDbContext>(options =>
       options.UseNpgsql(ConfigureCore.idPConnectionString));
builder.Services.AddDbContext<PortalDbContext>(options =>
       options.UseNpgsql(ConfigureCore.portalConnectionString));

builder.Services.AddSingleton<UploadFileService>();
builder.Services.AddSingleton<SettingService>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSingleton<RecordCarServices>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddSingleton<MenuListService>();
builder.Services.AddSingleton<PaymentOrderListService>();
builder.Services.AddSingleton<DisbursementHistoryServices>();
builder.Services.AddSingleton<AssignHealthChecklistServices>();
builder.Services.AddSingleton<WithdrawalRequestListServices>();
builder.Services.AddSingleton<ReportsPromoteAIListServices>();
builder.Services.AddSingleton<VerifyAdminIdentifyServices>();
builder.Services.AddAntDesign();
builder.Services.AddMatBlazor();

try
{


    builder.Services.AddHttpClient<SettingService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
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
    builder.Services.AddHttpClient<UserRoleService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
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

}
catch(Exception ex)
{

}


builder.Services.AddSweetAlert2();

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
