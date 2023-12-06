namespace sso.mms.helper.Configs
{
    public class HttpClientFactory
    {
        public static HttpClient CreateHttpClient(string baseAddress)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler
                .DangerousAcceptAnyServerCertificateValidator
            });
            httpClient.BaseAddress = new Uri(baseAddress);
            return httpClient;
        }
    }
}
