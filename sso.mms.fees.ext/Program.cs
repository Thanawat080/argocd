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
using sso.mms.fees.ext.Data;
using sso.mms.fees.ext.Providers.PromoteHealth.DetermineReferenceValue;
using sso.mms.fees.ext.Providers.PromoteHealth.BookHealthCheckup;
using sso.mms.fees.ext.Providers.Dental.RecordCar;
using sso.mms.fees.ext.Providers.PromoteHealth.RecordChecklist;
using sso.mms.fees.ext.Providers.PromoteHealth.DetermineDoctor;
using sso.mms.fees.ext.Providers.PromoteHealth.DisbursementHistory;
using sso.mms.fees.ext.Providers.Dental.TreatmentRecord;
using sso.mms.fees.ext.Providers.PromoteHealth.PayOrderHistory;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using sso.mms.fees.ext.Providers.Dental.HistoryWithdrawals;
using sso.mms.fees.ext.Providers.Dental.ListWithdrawals;
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
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<ReadTokenService>();
builder.Services.AddSingleton<MenuListService>();
builder.Services.AddSingleton<DetermineReferenceServices>();
builder.Services.AddSingleton<BookHealthCheckupServices>();
builder.Services.AddSingleton<RecordChecklistServices>();
builder.Services.AddSingleton<DetermineDoctorServices>();
builder.Services.AddSingleton<DisbursementHistoryServices>();
builder.Services.AddSingleton<PayOrderHistoryServices>();
builder.Services.AddSingleton<RecordCarServices>();
builder.Services.AddSingleton<TreatmentRecordServices>();
builder.Services.AddSingleton<HistoryWithdrawalsServices>();
builder.Services.AddSingleton<ListWithdrawalsServices>();

builder.Services.AddAntDesign();
builder.Services.AddMatBlazor();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
try
{


    builder.Services.AddHttpClient<SettingService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressPortalAdmin);
    });
    builder.Services.AddHttpClient<ReadTokenService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
    });
    builder.Services.AddHttpClient<UserRoleService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);
    });
    builder.Services.AddHttpClient<NotificationService>(client =>
    {
        client.BaseAddress = new Uri(ConfigureCore.baseAddressNotification);
    });

}
catch (Exception ex)
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
