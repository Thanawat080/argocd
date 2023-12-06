
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.Master;
using System.Collections.Generic;

namespace sso.mms.login.Services
{
    public class MasterProvinceService
    { 
        private readonly HttpClient httpClient;
        public MasterProvinceService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ProvinceM>> GetProvince()
        {
            var response = await httpClient.GetAsync("api/master/getProvince");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var convertRes = JsonConvert.DeserializeObject<List<ProvinceM>>(result);

                return convertRes!;
            }
            return null!;
        }

        public async Task<List<DistrictM>> GetDistrict(string ProvinceCode)
        {

            var response = await httpClient.GetAsync("api/master/getDistrict/" + $"{ProvinceCode}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var convertRes = JsonConvert.DeserializeObject<List<DistrictM>>(result);

                return convertRes!;

            }
            return null!;
        }
        public async Task<List<SubdistrictM>> GetSubDistrict(string DistrictCode)
        {
            var response = await httpClient.GetAsync("api/master/getSubDistrict/" + $"{DistrictCode}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var convertRes = JsonConvert.DeserializeObject<List<SubdistrictM>>(result);

                return convertRes!;
            }
            return null!;
        }
    }
}