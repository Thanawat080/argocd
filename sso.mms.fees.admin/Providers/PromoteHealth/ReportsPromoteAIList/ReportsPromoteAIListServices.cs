using sso.mms.fees.admin.ViewModels.Responses;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;

namespace sso.mms.fees.admin.Providers.PromoteHealth.ReportsPromoteAIList
{
    public class ReportsPromoteAIListServices
    {
        private readonly HttpClient httpClient;

        public ReportsPromoteAIListServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;

        }

        public async Task<ResponseList<AaiHealthWithdrawalHViewForAi>> GetWithdrawalForAi()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<ResponseList<AaiHealthWithdrawalHViewForAi>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/WithdrawalRequestListPromotet/getWithdrawalForAi");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<HospitalAi> GetAllHosForAI(string withdrawno)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<HospitalAi>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/WithdrawalRequestListPromotet/GetAllHosForAI?withdrawno={withdrawno}");
                return res;
            }
            catch
            {
                return null;
            }
        }
        public async Task<PersonForAi> GetAllPersonForAI(string withdrawNo, string hosCode)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<PersonForAi>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/WithdrawalRequestListPromotet/GetAllPersonForAI?withdrawNo={withdrawNo}&hosCode={hosCode}");
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}
