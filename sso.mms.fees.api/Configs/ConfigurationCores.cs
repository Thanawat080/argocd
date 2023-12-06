namespace sso.mms.fees.api.Configs
{
    public class ConfigurationCores
    {
        public static string ConfigENV = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Environment").Value!;
        public static string baseAddressIdpKeycloak = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection($"ExternalIP_{ConfigENV}:idp_keycloak").Value!;
        public static string NormalPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection($"AiConfig_{ConfigENV}:NormalInputPath").Value!;
        public static string ProActiveInputPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection($"AiConfig_{ConfigENV}:ProActiveInputPath").Value!;
        public static string ApiUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection($"AiConfig_{ConfigENV}:ApiUrl").Value!;
        public static string FeesApiAddress = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FeesApiAddress_" + ConfigENV + ":feesApiUrl").Value;
    }
}
