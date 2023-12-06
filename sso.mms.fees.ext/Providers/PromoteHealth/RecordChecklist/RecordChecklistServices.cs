using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using System.Net.Http;

namespace sso.mms.fees.ext.Providers.PromoteHealth.RecordChecklist
{
    public class RecordChecklistServices
    {
        private readonly HttpClient httpClient;

        public RecordChecklistServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ManageRecordChecklistView> GetChecklistByCheckupId(decimal checkupId)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ManageRecordChecklistView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/GetChecklistByCheckupId/{checkupId}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<GetReServeHView>> GetAllRecordChecklist(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<GetReServeHView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetRecordChecklist/GetAllRecordChecklist/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<GetRecordChecklistView>> GetAllRecordChecklistByUser(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<GetRecordChecklistView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetRecordChecklist/GetAllRecordChecklistByUser/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<AdminHos>> GetAllRoleAdminByHosCode(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AdminHos>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetRecordChecklist/GetAllRoleAdminByHosCode/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> CheckLoginAdmin(string username , string password)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<string>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/CheckLoginAdmin/{username}/{password}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<string> Saverecord(saverecord data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<saverecord>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/SaveRcord", data);
                if (result.IsSuccessStatusCode)
                {
                    return "success";
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


        public async Task<string> UpdateResult(ManageRecordChecklistView checklistView)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<ManageRecordChecklistView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/UpdateResult", checklistView);
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

                return ex.Message;
            }
        }


        public async Task<string> UpdateRecord(saverecord data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<saverecord>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageRecordChecklist/UpdateRecord", data);
                if (result.IsSuccessStatusCode)
                {
                    return "success";
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
