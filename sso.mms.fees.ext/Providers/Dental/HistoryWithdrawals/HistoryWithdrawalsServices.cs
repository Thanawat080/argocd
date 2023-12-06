using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;

namespace sso.mms.fees.ext.Providers.Dental.HistoryWithdrawals
{
    public class HistoryWithdrawalsServices
    {
        private readonly HttpClient httpClient;
        public HistoryWithdrawalsServices(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async Task<ResponseList<AaiDentalCheckH>> GetHistorys()
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ResponseList<AaiDentalCheckH>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Ext/HistoryWithdrawal/GetHistorys");
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
