using Blazorise;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using System.Data;
using System.Net.Http;
using System.Text.Json;

namespace sso.mms.fees.admin.Providers.PromoteHealth.AssignHealthChecklist
{
    public class AssignHealthChecklistServices
    {
        private readonly HttpClient httpClient;

        public AssignHealthChecklistServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<AaiHealthBudgetYearM>> GetAllBudYearM() 
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthBudgetYearM>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthBudgetYearM/GetAll");
            return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AaiHealthChecklistM>> GetAllCheckListM()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthChecklistM>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListM/GetAll");
                return res;
            }
            catch {
                return null;            
            }
        }


        public async Task<AaiHealthBudgetYearM> GetByBudYear(string? year)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<AaiHealthBudgetYearM>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthBudgetYearM/GetByBudYear/{year}");
                return res;
            }
            catch {
                return null;
            }
        }
        
        public async Task<List<AaiHealthChecklistM>> GetByYear(string year)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthChecklistM>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListM/GetByYear/{year}");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AaiHealthChecklistM> GetById(decimal? Checklistid)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<AaiHealthChecklistM>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListM/GetById/{Checklistid}");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AaiHealthCheckListDView>> GetByCheckListId(decimal? Checklistid)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthCheckListDView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListD/GetByCheckListId/{Checklistid}");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> saveEditType(ManageAssignHealthCheckListView data)
        {
            try
            {
                var res1 = await httpClient.PostAsJsonAsync<ManageAssignHealthCheckListView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/ManageAssignHealthCheckList/ManageTypeEdit", data);
                if (res1.IsSuccessStatusCode)
                {
                    var response1 = await res1.Content.ReadAsStringAsync();
                    return response1;
                }
                else {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> saveAddType(ManageAssignHealthCheckListView data)
        {
            try
            {
                var test = JsonSerializer.Serialize(data);
                var res1 = await httpClient.PostAsJsonAsync<ManageAssignHealthCheckListView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/ManageAssignHealthCheckList/ManageTypeCreate", data);
                if (res1.IsSuccessStatusCode)
                {
                    var response1 = await res1.Content.ReadAsStringAsync();
                    return response1;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        


    }
}
