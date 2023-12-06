using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;
using System.Data;
using System.Net.Http;

namespace sso.mms.fees.admin.Providers.PromoteHealth.AssignHealthChecklist
{
    public class WithdrawalRequestListServices
    {
        private readonly HttpClient httpClient;

        public WithdrawalRequestListServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response<AaiHealthCheckupHView>> GetByCheckId(int CheckupId)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync <Response<AaiHealthCheckupHView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/WithdrawalRequestListPromotet/getByCheckId/{CheckupId}");
                return res;
            }
            catch
            {
                return null;
            }
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
    }
}
        