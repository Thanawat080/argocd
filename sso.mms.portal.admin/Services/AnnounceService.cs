using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace sso.mms.portal.admin.Services
{
    public class AnnounceService
    {
        private readonly HttpClient httpClient;

        public ResponseUpload responseUpload;

        public AnnounceService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
            //this.httpClient = httpClient;
        }
        public async Task<List<AnnounceT>> GetAnnounceT()
        {
            var response = await httpClient.GetAsync("api/Announce/GetAnnounceT");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var TakeAnnounceT = JsonConvert.DeserializeObject<List<AnnounceT>>(result);

                    return TakeAnnounceT;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseModel> INsertAnnounceT(AnnounceModel announcement)
        {
            if (announcement.File != null)
            {
                responseUpload = await UploadFile(announcement.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }

            AnnounceT AnnounceT = new AnnounceT()
            {
                Title = announcement.Title,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                ImagePath = responseUpload.Path_Url,
                ImageFile = responseUpload.FileName,
                UpdateBy = announcement.UpdateBy,
                CreateBy = announcement.CreateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsStatus = 1,
                IsActive = true,
                ActiveStatus = announcement.ActiveStatus
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Announce/InsertAnnounceT", AnnounceT);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var success = new ResponseModel
                    {
                        issucessStatus = true,
                        statusCode = "200",
                        statusMessage = "insert your record success"
                    };

                    return success;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
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
        public async Task<ResponseModel>EditAnnounceT(AnnounceModel announceTs)
        {
            AnnounceT announceT = new AnnounceT();
            if (announceTs.File != null)
            {
                responseUpload = await UploadFile(announceTs.File);
                announceT = new AnnounceT()
                {
                    Id = announceTs.Id,
                    Title = announceTs.Title,
                    StartDate = announceTs.StartDate,
                    EndDate = announceTs.EndDate,
                    ImagePath = responseUpload.Path_Url,
                    ImageFile = responseUpload.FileName,
                    UpdateBy = announceTs.UpdateBy,
                    CreateBy = announceTs.CreateBy,
                    CreateDate = announceTs.CreateDate,
                    UpdateDate = DateTime.Now,
                    IsStatus = 1,
                    IsActive = true,
                    ActiveStatus = announceTs.ActiveStatus
                };
            }
            else
            {
                
                announceT = new AnnounceT()
                {
                    Id = announceTs.Id,
                    Title = announceTs.Title,
                    StartDate = announceTs.StartDate,
                    EndDate = announceTs.EndDate,
                    ImagePath = announceTs.ImagePath,
                    ImageFile = announceTs.ImageFile,
                    UpdateBy = announceTs.UpdateBy,
                    CreateBy = announceTs.CreateBy,
                    CreateDate = announceTs.CreateDate,
                    UpdateDate = DateTime.Now,
                    IsStatus = 1,
                    IsActive = true,
                    ActiveStatus = announceTs.ActiveStatus
                };
            }

           

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Announce/EditAnnounce", announceT);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var success = new ResponseModel
                    {
                        issucessStatus = true,
                        statusCode = "200",
                        statusMessage = "insert your record success"
                    };

                    return success;
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
