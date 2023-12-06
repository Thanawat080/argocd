using Microsoft.AspNetCore.Hosting;

namespace sso.mms.helper.Configs
{
    public class ConfigureCore
    {
        public static string ConfigENV = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Environment").Value;
        public static string SecretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SECRET_KEY_PASSWORD").Value;

        public static string idPConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings_" + ConfigENV + ":idp").Value;
        public static string portalConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings_" + ConfigENV + ":portal").Value;
        public static string batchSyncConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings_" + ConfigENV + ":batchsync").Value;


        public static string baseAddressChat = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":chat").Value;
        public static string baseAddressLogin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":login").Value;
        public static string baseAddressNews = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":news").Value;
        public static string baseAddressNotification = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":notification").Value;
        public static string baseAddressPortalAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":portal_admin").Value;
        public static string baseAddressPortalExt = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":portal_ext").Value;
        public static string baseAddressIdpKeycloak= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":idp_keycloak").Value;
        public static string baseAddressOtherService = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":other_service").Value;
        public static string baseAddressCkanOpenD = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":ckan_opend").Value;
        public static string baseAddressbatchSync = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":batch_sync").Value;
        public static string baseDopavalidateurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":dopa_validate_url").Value;
        public static string baseDopaverification = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":dopa_verification_url").Value;
        public static string baseEdoc = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BaseAddress_" + ConfigENV + ":edoc").Value;

        public static string secretKeyRealmGroupHospital = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecretKeyRealmGroup_" + ConfigENV + ":sso-mms-hospital").Value;
        public static string secretKeyRealmGroupAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecretKeyRealmGroup_" + ConfigENV + ":sso-mms-admin").Value;
        public static string secretKeyRealmGroupAuditor = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecretKeyRealmGroup_" + ConfigENV + ":sso-mms-auditor").Value;


        public static string redirectChat = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":chat").Value;
        public static string redirectLogin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":login").Value;
        public static string redirectsNews = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":news").Value;
        public static string redirectNotification = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":notification").Value;
        public static string redirectsPortalAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":portal_admin").Value;
        public static string redirectPortalExt = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":portal_ext").Value;
        public static string redirectCkanOpenD = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":ckan_opend").Value;
        public static string redirectOtherService = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":other_service").Value;
        public static string redirectOtherServiceAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":other_service_admin").Value;
        public static string redirectFeesAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":fees-admin").Value;
        public static string redirectFeesExt = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":fees-ext").Value;
        public static string redirectRpt = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Redirect_" + ConfigENV + ":rpt").Value;

        public static string EmailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":EmailFrom").Value;
        public static string SmtpHost = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":SmtpHost").Value;
        public static string SmtpPort = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":SmtpPort").Value;
        public static string SmtpUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":SmtpUser").Value;
        public static string SmtpPass = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":SmtpPass").Value;
        public static string DisplayName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MailSettings_" + ConfigENV + ":DisplayName").Value;

        public static string FeesApiAddress = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FeesApiAddress_" + ConfigENV + ":feesApiUrl").Value;


        public static string apiKeyEdoc = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("apiKey_" + ConfigENV + ":edoc").Value;


        public static string SiteName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteName").Value;

        public static string SetTimeSecoundRefreshToken = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SetTimeSecoundRefreshToken").Value;
    }
}
