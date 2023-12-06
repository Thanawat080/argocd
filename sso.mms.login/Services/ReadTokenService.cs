using MatBlazor;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.login.ViewModels;

namespace sso.mms.login.Services
{

    public class ReadTokenService
    {
        private readonly HttpClient httpClient;
        public ReadTokenService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseShortToken> ReadToken(string shortToken)
        {
            var response = await httpClient.GetAsync("api/Login/GetToken/" + $"{shortToken}");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var convertRes = JsonConvert.DeserializeObject<ResponseShortToken>(result.Result);

                    return convertRes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
    }
}
