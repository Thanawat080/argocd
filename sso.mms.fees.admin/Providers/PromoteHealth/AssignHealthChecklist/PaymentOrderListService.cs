using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using System.Data;
using System.Net.Http;

namespace sso.mms.fees.admin.Providers.PromoteHealth.PaymentOrderList
{
    public class PaymentOrderListService
    {
        private readonly HttpClient httpClient;

        public PaymentOrderListService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<GetPaymentOrderList>> GetAllBudYearM()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<GetPaymentOrderList>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPaymentOList");
            return res;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GetPaymentOrderList> GetByWithdrawalNo(string wdNo)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<GetPaymentOrderList>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetByWithdrawalNo?withdrawalNo={wdNo}");
                return res;
            }
            catch
            {
                return null;
            }
        } 
        public async Task<List<GetPaymentOrderList>> GetHospByWitdraw(string wdNo)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync <List<GetPaymentOrderList>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetHospByWitdraw?withdrawalNo={wdNo}");
                return res;
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<AaiHealthCheckupHViewModel>> GetPersonPayorderSetNoBy(string payordersetno)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<AaiHealthCheckupHViewModel>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPersonPayorderSetNoBy?payordersetno={payordersetno}");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AaiHealthCheckupHViewModel>> GetPerson(string wdNo , string hoscode)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync <List<AaiHealthCheckupHViewModel>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPerson?withdrawalNo={wdNo}&hospCode={hoscode}");
                return res;
            }
            catch
            {
                return null;
            }
        }


        public async Task<string> Save(SaveOrderT data)
        {
            try
            {
                var res = await httpClient.PostAsJsonAsync<SaveOrderT>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/SavePayorder",data);
                if (res.IsSuccessStatusCode)
                {
                    var response1 = await res.Content.ReadAsStringAsync();
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


        public async Task<List<GetPayOrderListT>> GetPayOrder()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<GetPayOrderListT>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPayOrder");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<GetPayOrderListT>> GetPayOrderHis()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<List<GetPayOrderListT>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/GetPayOrderHis");
                return res;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> UpdatePayOrder(List<GetPayOrderListT> data)
        {
            try
            {
                var res = await httpClient.PostAsJsonAsync<List<GetPayOrderListT>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/PaymentOrderList/UpdatePayOrder", data);
                if (res.IsSuccessStatusCode)
                {
                    var response1 = await res.Content.ReadAsStringAsync();
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
