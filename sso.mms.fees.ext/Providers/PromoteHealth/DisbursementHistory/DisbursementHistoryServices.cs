using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using NPOI.HPSF;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using sso.mms.helper.ViewModels;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace sso.mms.fees.ext.Providers.PromoteHealth.DisbursementHistory
{
    public class DisbursementHistoryServices
    {
        private readonly HttpClient httpClient;

        public DisbursementHistoryServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<WithdrawalView>> GetWithdrawalByHoscode(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<WithdrawalView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageWithdrawal/GetWithdrawalByHoscode/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ViewAaiHealthCheckupH>> GetCheckupHInWithdrawal(string hoscode, string withdrawalNo)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<ViewAaiHealthCheckupH>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageWithdrawal/GetCheckupHInWithdrawal?hoscode={hoscode}&withdrawalNo={withdrawalNo}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> EditWithdrawalDoc(WithdrawalDoc dataDoc)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = dataDoc.File!.OpenReadStream())
                    {
                        var fileContent = new StreamContent(fileStream);
                        formData.Add(new StringContent(dataDoc.WithdrawalNo), "WithdrawalNo");
                        formData.Add(new StringContent(dataDoc.HospitalCode), "HospitalCode");
                        formData.Add(fileContent, "File", dataDoc.File.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageWithdrawal/EditWithdrawalDoc", formData);
                        
                        if (response.IsSuccessStatusCode)
                        {
                       
                            return "success";
                        }
                        else
                        {
                            return "fail";
                        }
                    }
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
