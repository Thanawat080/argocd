using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;

namespace sso.mms.fees.ext.Providers.Dental.ListWithdrawals
{
    public class ListWithdrawalsServices
    {
        private readonly HttpClient httpClient;
        public ListWithdrawalsServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseList<AaiDentalCheckH>> GetListWithdrawals()
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ResponseList<AaiDentalCheckH>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Ext/ListWithdrawals/GetListWithdrawals");
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
