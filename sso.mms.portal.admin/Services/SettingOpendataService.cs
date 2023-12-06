using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sso.mms.portal.admin.Services
{
    public class SettingOpendataService
    {
        private readonly HttpClient httpClient;
        public ResponseUpload responseUpload;
        public SettingOpendataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseModel> EditDataSetting(List<SettingOpendataT> SettingOpendata)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/SettingOpendata/EditSettingOpendata", SettingOpendata);
                var res = response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                {
                    var responseObj = new ResponseModel
                    {
                        statusCode = "400",
                        issucessStatus= false,
                        statusMessage = "Bad Request"
                    };
                    return responseObj;
                }
                else
                {
                    var responseObj = new ResponseModel
                    {
                        statusCode = "200",
                        issucessStatus = true,
                        statusMessage = "สำเร็จ"
                    };
                    return responseObj;
                }
            }
            catch (Exception ex)
            {
                var responseObj = new ResponseModel
                {
                    statusCode = "400",
                    issucessStatus = false,
                    statusMessage = "Bad Request "+ ex.Message
                };
                return responseObj;
            }
        }
        public async Task<List<SettingOpendataT>> GetSettingOpendataT()
        {
            try
            {
                var response = await httpClient.GetAsync("api/SettingOpendata/GetSettingOpenDataT");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var getSettingOpendata = JsonConvert.DeserializeObject<List<SettingOpendataT>>(result);

                    return getSettingOpendata;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseModel> AddSettingOpendata(SettingOpendataModel addSettingOpendata)
        {

            if (addSettingOpendata.File != null)
            {
                responseUpload = await UploadFile(addSettingOpendata.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }


            SettingOpendataT settingOpendata = new SettingOpendataT()
            {
                Title = addSettingOpendata.Title,
                Url = addSettingOpendata.Url,
                Detail = addSettingOpendata.Detail,
                UploadImagePath = responseUpload.Path_Url,
                UploadImageName = responseUpload.FileName,
                UploadFileName = null,
                UploadFilePath = null,
                IsActive = true,
                IsStatus = 1,
                CreateDate = DateTime.Now,
                CreateBy = addSettingOpendata.CreateBy,
                UpdateDate = DateTime.Now,
                UpdateBy = addSettingOpendata.UpdateBy,
                ShowStatus = addSettingOpendata.ShowStatus
            };

            var response = await httpClient.PostAsJsonAsync("api/SettingOpendata/AddOpenDataT", settingOpendata);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var success = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "insert your record success"
                };


                return success;
            }
            var fail = new ResponseModel
            {
                issucessStatus = false,
                statusCode = "400",
                statusMessage = "insert your record fail"
            };
            return fail;
        }
        public async Task<ResponseUpload> UploadFile(IBrowserFile file)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = file.OpenReadStream(maxAllowedSize : 5242880))
                    {
                        var fileContent = new StreamContent(fileStream);
                        formData.Add(fileContent, "file", file.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync("api/SettingOpendata/UploadImage", formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;

                            var responseUpload = JsonConvert.DeserializeObject<ResponseUpload>(result);

                            var success = new ResponseUpload
                            {
                                FileName = responseUpload?.FileName,
                                Path_Url = responseUpload?.Path_Url,
                            };

                            return success;
                        }
                        else
                        {
                            var fail = new ResponseUpload
                            {
                                FileName = "",
                                Path_Url = "",
                            };
                            return fail;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var fail = new ResponseUpload
                {
                    Error = ex.Message,
                    FileName = "",
                    Path_Url = "",
                };
                return fail;
            }

        }
    }
}
