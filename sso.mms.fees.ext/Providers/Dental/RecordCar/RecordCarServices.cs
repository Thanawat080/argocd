using AntDesign;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using System.Net.Http.Json;

namespace sso.mms.fees.ext.Providers.Dental.RecordCar
{
    public class RecordCarServices
    {
        private readonly HttpClient httpClient;
        private readonly UploadFileService uploadFileService;
        public RecordCarServices(HttpClient httpClient, UploadFileService uploadFileService)
        {
            this.httpClient = httpClient;
            this.uploadFileService = uploadFileService;
        }

        public async Task<List<AaiDentalCarHView>> GetAll()
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AaiDentalCarHView>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Ext/CarRecord/GetAll");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> AddDentalRecord(InsertDataViewModel carData)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<InsertDataViewModel>($"{ConfigureCore.FeesApiAddress}/api/Dental/Ext/CarRecord/AddCarRecord", carData);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return $"error {response.StatusCode}";
                }
              
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
  
        public async Task<string> UpdateCarHChangeDes(ChangeDesViewModel carHData)
        {
            try
            {
              
                var result = await httpClient.PostAsJsonAsync($"{ConfigureCore.FeesApiAddress}/api/Dental/Ext/CarRecord/UpdateCarHChangeDes", carHData);
                if (result.IsSuccessStatusCode)
                {
                    return "success";
                }
                else
                {
                    return "false";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
        public async Task<ResponseList<AdbMProvince>> GetProvince()
        { 
            var response = new ResponseList<AdbMProvince>();
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AdbMProvince>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Base/Province/GetProvince");
                if (result != null || result.Count() != 0)
                {
                    response = new ResponseList<AdbMProvince>()
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "Success"
                    };
                    return response;
                }
                else
                {

                    response = new ResponseList<AdbMProvince>()
                    {
                        ResultList = null,
                        Status = 401,
                        Message = "no content"
                    };
                    return response; ;
                }

            }
            catch (Exception ex)
            {
                response = new ResponseList<AdbMProvince>()
                {
                    ResultList = null,
                    Status = 500,
                    Message = "Error " +ex.Message
                };
         
                return response;
            }
        }
        public async Task<ResponseList<AdbMDistrict>> GetDistrict(int provId)
        { 
            var response = new ResponseList<AdbMDistrict>();
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AdbMDistrict>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Base/Province/GetDistrict?provinceId={provId}");
                if (result != null || result.Count() != 0)
                {
                    response = new ResponseList<AdbMDistrict>()
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "Success"
                    };
                    return response;
                }
                else
                {

                    response = new ResponseList<AdbMDistrict>()
                    {
                        ResultList = null,
                        Status = 401,
                        Message = "no content"
                    };
                    return response; ;
                }

            }
            catch (Exception ex)
            {
                response = new ResponseList<AdbMDistrict>()
                {
                    ResultList = null,
                    Status = 500,
                    Message = "Error " +ex.Message
                };
         
                return response;
            }
        }
         public async Task<ResponseList<AdbMSubdistrict>> GetSubDistrict(int distId)
        { 
            var response = new ResponseList<AdbMSubdistrict>();
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AdbMSubdistrict>>($"{ConfigureCore.FeesApiAddress}/api/Dental/Base/Province/GetSubDistrict?disId={distId}");
                if (result != null || result.Count() != 0)
                {
                    response = new ResponseList<AdbMSubdistrict>()
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "Success"
                    };
                    return response;
                }
                else
                {

                    response = new ResponseList<AdbMSubdistrict>()
                    {
                        ResultList = null,
                        Status = 401,
                        Message = "no content"
                    };
                    return response; ;
                }

            }
            catch (Exception ex)
            {
                response = new ResponseList<AdbMSubdistrict>()
                {
                    ResultList = null,
                    Status = 500,
                    Message = "Error " +ex.Message
                };
         
                return response;
            }
        }

        public async Task<string> UploadFile([FromForm] List<IFormFile>? upload)
        {
            string Uploadurl;

           
            //Uploadurl = $"{ConfigureCore.FeesApiAddress}/Files/Dental";
            Uploadurl = $"https://localhost:7001/Files/Dental/";

            (string errorMessage, string imageName) = await uploadFileService.UploadImage(upload, "Dental");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            try
            {
                var success = new ResponseUpload
                {
                    //Uploaded = 1,
                    FileName = imageName,
                    Path_Url = Uploadurl + imageName,
                    Url = Uploadurl + imageName,
                };

                return success.Path_Url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
