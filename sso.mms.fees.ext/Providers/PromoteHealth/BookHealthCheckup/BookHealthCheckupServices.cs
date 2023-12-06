using Newtonsoft.Json;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using System.Net.Http.Json;

namespace sso.mms.fees.ext.Providers.PromoteHealth.BookHealthCheckup
{
    public class BookHealthCheckupServices
    {
        private readonly HttpClient httpClient;

        public BookHealthCheckupServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ManageBookHealthCheckupView> GetReserveById(int reserveId)
        {

            try
            {
                var result = await httpClient.GetFromJsonAsync<ManageBookHealthCheckupView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetReserveH/GetReserveById/{reserveId}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<List<GetReServeHView>> GetReserve(string hoscode)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<GetReServeHView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/GetReserveH/GetAll/{hoscode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<CheckPermissionCheckListView>> CheckPermission(List<person> data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<List<person>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/CheckPermissionCheckList/CheckPermission", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<CheckPermissionCheckListView>>(response);
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<apiview850> CheckPersonFrom850Api(string identificationnumber)
        {
            try
            {
                var result = await httpClient.GetAsync($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/CheckPermissionCheckList/CheckPersonFrom850Api?identificationnumber={identificationnumber}");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<apiview850>(response);
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        

        public async Task<string> CreateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<ManageBookHealthCheckupView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageBookHealthCheckup/CreateBookCheckup", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> UpdateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<ManageBookHealthCheckupView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageBookHealthCheckup/UpdateBookCheckup", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> UpdateReserveStatus(decimal reserveId, string status)
        {
            try
            {
                var result = await httpClient.GetAsync($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthReserveH/UpdateReserveStatus?reserveId={reserveId}&status={status}");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}
