using sso.mms.helper.Configs;
using System.Net.Http;

namespace sso.mms.fees.admin.Providers.VerifyAdminIdentify
{
    public class VerifyAdminIdentifyServices
    {
        private readonly HttpClient httpClient;

        public VerifyAdminIdentifyServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> CheckLoginAdmin(string username, string password)
        {
            try
            {
                var result = await httpClient.GetAsync($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/CheckLoginAdmin/{username}/{password}");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

}
