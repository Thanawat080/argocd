using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;

namespace sso.mms.fees.ext.Providers.PromoteHealth.PayOrderHistory
{
    public class PayOrderHistoryServices
    {
        private readonly HttpClient httpClient;

        public PayOrderHistoryServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PayOrderView>> GetPayOrderByHoscode(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<PayOrderView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetViewPayOrder/GetByHoscode/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<AaiHealthCheckupHViewModel>> GetPersonPayorderSetNoBy(string payordersetno, string? hoscode)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthCheckupHViewModel>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPersonPayorderSetNoBy?payordersetno={payordersetno}&hoscode={hoscode}");
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}
