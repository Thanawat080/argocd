using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.notification.ViewModel;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.portal.ext.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace sso.mms.portal.ext.Services
{
    public class CertificateService
    {
        private readonly HttpClient httpClient;
        public ResponseUpload responseUpload;
        public CertificateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CertificateT> GetCertificate(string? hospitalCode)
        {
            try
            {
                var responseCertificateT = await httpClient.GetAsync("api/Certificate/GetCertificate/" + $"{hospitalCode}");
                responseCertificateT.EnsureSuccessStatusCode();
                if (responseCertificateT.IsSuccessStatusCode)
                {   
                    if(responseCertificateT != null)
                    {
                        var result = responseCertificateT.Content.ReadAsStringAsync().Result;
                        var Certificate = JsonConvert.DeserializeObject<CertificateT>(result);
                        return Certificate;
                    }
                    else
                    {
                        return null;
                    }
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
        public async Task<ResponseModel> AddCertificate(RequestCertificate addCertificate, string getusername, string? getHospitalMCode)
        {

            if (addCertificate.File != null)
            {
                responseUpload = await UploadFile(addCertificate.File);
                CertificateT certificate = new CertificateT()
                {
                    UploadImagePath = responseUpload.Path_Url,
                    //UploadImageName = responseUpload.FileName,
                    //UploadFileName = null,
                    //UploadFilePath = null,
                    IsActive = true,
                    IsStatus = 1,
                    CreateBy = getusername,
                    UpdateDate = DateTime.Now,
                    UpdateBy = getusername,
                    HospitalMCode = getHospitalMCode
                };
                var response = await httpClient.PostAsJsonAsync("api/Certificate/AddCertificate", certificate);
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
                else
                {
                    var fail = new ResponseModel
                    {
                        issucessStatus = false,
                        statusCode = "400",
                        statusMessage = "insert your record fail"
                    };
                    return fail;
                }
            }
            else
            {
                responseUpload = new ResponseUpload();
                var fail = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = "insert your record fail"
                };
                return fail;
            }
                     
        }
        public async Task<ResponseUpload> UploadFile(IBrowserFile file)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = file.OpenReadStream(maxAllowedSize: 5242880))
                    {
                        var fileContent = new StreamContent(fileStream);
                        formData.Add(fileContent, "file", file.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync("api/Certificate/UploadCertificate", formData);
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