using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Configs;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Services.PromoteHealth.Admin;
using sso.mms.fees.api.Services.PromoteHealth.Base;
using sso.mms.fees.api.Services.PromoteHealth.EXT;
using sso.mms.helper.Configs;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.helper;
using sso.mms.helper.Services;

namespace sso.mms.fees.api.Startup.DependencyInjectionSetup
{
    public static class PromoteHealthSetup
    {
        public static ConnectionStringModels? ConnectionStrings { get; set; }
        public static ConfigurationManager PromoteHealthConfigurationBuilder(this ConfigurationManager configurationBuilder)
        {
            
            var Environment = configurationBuilder.GetSection("Environment").Value;
            ConnectionStrings = configurationBuilder.GetSection($"ConnectionStrings_{Environment}").Get<ConnectionStringModels>();

            return configurationBuilder;
        }
        public static IServiceCollection RegisterPromoteHealthServices(this IServiceCollection services)
        {

            services.AddTransient<IAaiHealthBudgetYearMBaseServices, AaiHealthBudgetYearMServices>();
            services.AddTransient<IAaiHealthCheckListMBaseServices, AaiHealthCheckListMServices>();
            services.AddTransient<IAaiHealthCheckListDBaseServices, AaiHealthCheckListDServices>();
            services.AddTransient<IManageAssignHealthCheckListAdminServices, ManageAssignHealthCheckListServices>();
            services.AddTransient<IAaiHealthSetRefChecklistCfgBaseServices, AaiHealthSetRefChecklistCfgServices>();
            services.AddTransient<IManageCheckListD, ManageCheckListDService>();
            services.AddTransient<IAaiHealthSetRefDoctorBaseServices, AaiHealthSetRefDoctorCfgServices>();
            services.AddTransient<IManageAaiHealthSetRefDoctorCfg, ManageAaiHealthSetRefDoctorCfgServices>();
            services.AddTransient<IManageSetRefNicknameCfgExt, ManageSetRefNicknameCfgServices>();
            services.AddTransient<ISaveDetermineReferenceValueExt, SaveDetermineReferenceServices>();
            services.AddTransient<IGetReserveHExtServices, GetReserveHServices>();
            services.AddTransient<ICheckPermissionCheckListExtServices, CheckPermissionCheckListServices>();
            services.AddTransient<IAlreadyCheckListBaseServices, AlreadyCheckListServices>();
            services.AddTransient<IManageBookHealthCheckupExtServices, ManageBookHealthCheckupServices>();
            services.AddTransient<IGetRecordChecklistExtServices, GetRecordChecklistServices>();
            services.AddTransient<IAaiHealthReserveHBaseServices, AaiHealthReserveHServices>();
            services.AddTransient<IManageRecordChecklistExtServices, ManageRecordChecklistServices>();
            services.AddTransient<IGetViewPayOrderExtServices, GetViewPayOrderServices>();
            services.AddTransient<IManageWithdrawalExtServices, ManageWithdrawalServices>();
            services.AddTransient<IUploadFileService, UploadFileService>();
            services.AddHttpClient<ManageRecordChecklistServices>(client =>
            {
                client.BaseAddress = new Uri(ConfigurationCores.baseAddressIdpKeycloak);
            });
            services.AddTransient<IWithdrawalRequestListPromoteAdminServices, WithdrawalRequestListPromoteServices>();
            services.AddTransient<IDisbursementHistoryPromoteAdminServices, DisbursementHistoryPromoteServices>();
            services.AddTransient<IPaymentOrderListService, PaymentOrderListService>();
            services.AddTransient<IGeneratePreAuditServices, GeneratePreAuditServices>();
            services.AddTransient<IManageCallBackService, ManageCallBackService>();
            services.AddHttpClient<IGeneratePreAuditServices, GeneratePreAuditServices>(client =>
            {
                client.BaseAddress = new Uri(ConfigurationCores.ApiUrl);
            });

            services.AddDbContext<PromoteHealthContext>(options =>
            options.UseOracle($"{ConnectionStrings!.PromoteHealthContext}"));



            return services;
        }

       
    }
}
