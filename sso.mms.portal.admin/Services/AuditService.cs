using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;
using System.Net.Http.Headers;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;

namespace sso.mms.portal.admin.Services
{
    public class AuditSevices
    {
        private readonly HttpClient httpClient;
        public ResponseUpload responseUpload;
        public AuditSevices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseModel> AddAudit(AuditorUserModel audit)
        {
            if (audit.File != null)
            {
                responseUpload = await UploadFile(audit.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }
            AuditorUserM AuditM = new AuditorUserM()
            {
                PrefixMCode= audit.PrefixMCode,
                FirstName = audit.FirstName,
                MiddleName = audit.MiddleName,
                LastName = audit.LastName,
                IdenficationNumber = audit.IdenficationNumber,
                CertNo = audit.CertNo,
                Position = audit.Position,
                StartDate = audit.StartDate,
                ExpireDate = audit.ExpireDate,
                Birthdate = audit.Birthdate,
                Email = audit.Email,
                Mobile = audit.Mobile,
                IsStatus = audit.IsStatus,
                IsActive = true,
                ImagePath = responseUpload.Path_Url,
                ImageName = responseUpload.FileName,

            };
            var response = await httpClient.PostAsJsonAsync("api/audit/AddAudit", AuditM);
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

        public async Task<ResponseModel> DeleteAudit(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var responseDel = await httpClient.GetAsync("api/audit/DeleteAudit/"+id);
                response = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "Delete success",
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message,
                };
                return response;
            }
        }
        public async Task<List<AuditorUserM>> GetAudit()
        {

            var response = await httpClient.GetAsync("api/audit/GetAudit");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var auditList = JsonConvert.DeserializeObject<List<AuditorUserM>>(result);

                    return auditList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null; 
            }
        } 
        public async Task<ResponseModel> EditAuditById(AuditorUserModel audit , int Id ) 
        {
            AuditorUserM AuditM = new AuditorUserM();
            if (audit.File != null)
            {
                responseUpload = await UploadFile(audit.File);
                AuditM = new AuditorUserM()
                {
                    Id = Id,
                    PrefixMCode = audit.PrefixMCode,
                    FirstName = audit.FirstName,
                    MiddleName = audit.MiddleName,
                    LastName = audit.LastName,
                    IdenficationNumber = audit.IdenficationNumber,
                    CertNo = audit.CertNo,
                    Position = audit.Position,
                    StartDate = audit.StartDate,
                    ExpireDate = audit.ExpireDate,
                    Birthdate = audit.Birthdate,
                    Email = audit.Email,
                    Mobile = audit.Mobile,
                    IsStatus = audit.IsStatus,
                    IsActive = true,
                    ImagePath = responseUpload.Path_Url,
                    ImageName = responseUpload.FileName,
                    UpdateBy = audit.UpdateBy

                };
            }
            else
            {
                AuditM = new AuditorUserM()
                {
                    Id = Id,
                    PrefixMCode = audit.PrefixMCode,
                    FirstName = audit.FirstName,
                    MiddleName = audit.MiddleName,
                    LastName = audit.LastName,
                    IdenficationNumber = audit.IdenficationNumber,
                    CertNo = audit.CertNo,
                    Position = audit.Position,
                    StartDate = audit.StartDate,
                    ExpireDate = audit.ExpireDate,
                    Birthdate = audit.Birthdate,
                    Email = audit.Email,
                    Mobile = audit.Mobile,
                    IsStatus = audit.IsStatus,
                    IsActive = true,
                    ImagePath = audit.ImagePath,
                    ImageName = audit.ImageName,
                    UpdateBy = audit.UpdateBy

                };
            }
           
            var response = await httpClient.PostAsJsonAsync($"api/audit/EditAudit",AuditM);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var success = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "Update your record success"
                };
                return success;
            }
            var fail = new ResponseModel
            {
                issucessStatus = false,
                statusCode = "400",
                statusMessage = "Update your record fail"
            };
            return fail;
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
                        formData.Add(fileContent, "upload", file.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync("api/audit/UploadFile", formData);

                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;

                            var responseUpload = JsonConvert.DeserializeObject<ResponseUpload>(result);

                            var success = new ResponseUpload
                            {
                                FileName = responseUpload.FileName,
                                Path_Url = responseUpload.Path_Url,
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

        public async Task<AuditorUserModel> GetAuditById(int Id)
        {

            var response = await httpClient.GetAsync($"api/audit/getAuditById?auditorId={Id}");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var auditList = JsonConvert.DeserializeObject<AuditorUserModel>(result);

                    return auditList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
