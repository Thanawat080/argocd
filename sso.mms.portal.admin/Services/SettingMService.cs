using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;
using sso.mms.portal.admin.Pages.EditBanner;

namespace sso.mms.portal.admin.Services
{
    public class SettingMService
    {
        private readonly HttpClient httpClient;
        public ResponseUpload responseUpload;
        public SettingMService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SettingM>> GetSettingM()
        {
            var response = await httpClient.GetAsync("api/settingm/getsetting");
            response.EnsureSuccessStatusCode();
            try
            {

                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<SettingM>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<string> savesetting(List<SettingM> data)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/settingm/savesetting", data);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
                else {
                    return "dontsuccess";
                }

                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<CheckSettingModel>> GetStatusMSetting(string text)
        {
            try
            {
                var response = await httpClient.GetAsync("api/settingm/getstatussetting?type=" + $"{text}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<CheckSettingModel>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
