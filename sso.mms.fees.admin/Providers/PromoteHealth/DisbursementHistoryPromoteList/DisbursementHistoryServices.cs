using System;
using sso.mms.fees.admin.ViewModels.Responses;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;

namespace sso.mms.fees.admin.Providers.PromoteHealth.DisbursementHistoryPromoteList
{
	public class DisbursementHistoryServices
	{

        private readonly HttpClient httpClient;


        public DisbursementHistoryServices(HttpClient httpClient)
		{
            this.httpClient = httpClient;
            			 
		}

        public async Task<ResponseList<PayOrderHistoryView>> GetPaymentOrderHistory()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<ResponseList<PayOrderHistoryView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/DisbursementHistoryPromote/GetDisbursementList");
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}

