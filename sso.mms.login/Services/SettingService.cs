using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.ViewModels;
using System.Net.Http;

namespace sso.mms.login.Services
{
    public class SettingService
    {
        private readonly HttpClient httpClient;
        public SettingService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CheckSettingModel> GetStatusMSetting(string text)
        {
            try
            {
                var response = await httpClient.GetAsync("api/setting/getstatussetting/" + $"{text}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<CheckSettingModel>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HospitalUserM> GetHosuserByUsername(string text)
        {
            try
            {
                var response = await httpClient.GetAsync("api/setting/gethosuserbyusername/" + $"{text}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<HospitalUserM>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
