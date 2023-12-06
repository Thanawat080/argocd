using DocumentFormat.OpenXml.Office2010.Excel;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;
using System.Net.Http.Json;
using Oracle.ManagedDataAccess.Types;

namespace sso.mms.fees.ext.Providers.Dental.TreatmentRecord
{
    public class TreatmentRecordServices
    {
        private readonly HttpClient httpClient;

        public TreatmentRecordServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseList<AaiDentalCheckHView>> TreatmentRecordList()
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ResponseList<AaiDentalCheckHView>>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/TreatmentRecordList");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task <string> TreatmentCreate(AaiDentalCheckHView data)
        {
            try 
            {
                var res = await httpClient.PostAsJsonAsync<AaiDentalCheckHView>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/TreatmentCreate", data);
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
            catch(Exception ex)
            {
                return ex.Message;
            }
        } public async Task <string> DentalCheckupCreate(AaiDentalCheckDView data)
        {
            try 
            {
                var res = await httpClient.PostAsJsonAsync<AaiDentalCheckDView>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/DentalCheckupCreate", data);
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
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<Response<AaiDentalCheckHView>> TreatmentRecordById (int id)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<Response<AaiDentalCheckHView>>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/TreatmentRecordById/" +id);

                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<apiview850> api850(string personid)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<apiview850>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/api850/" + personid);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResponseList<AaiDentalToothTypeMView>> ToothList()
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ResponseList<AaiDentalToothTypeMView>>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/ToothList");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResponseList<AaiDentalCheckDView>> CheckDList(int id )
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<ResponseList<AaiDentalCheckDView>>($"{ConfigureCore.FeesApiAddress}/api/Dental/ext/TreatmentRecord/CheckDList/"+id);
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
