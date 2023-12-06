using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using sso.mms.notification.ViewModel;
using sso.mms.notification.ViewModel.Response;
using sso.mms.portal.admin.Pages;
using sso.mms.portal.admin.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace sso.mms.portal.admin.Services
{
    public class BannerService
    {
        private readonly HttpClient httpClient;
        public ResponseUpload responseUpload;

        public BannerService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
        }
        public async Task<List<BannerT>> GetBannersT()
        {
            var response = await httpClient.GetAsync("api/banner/GetBannersT");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var Bannerlist = JsonConvert.DeserializeObject<List<BannerT>>(result);

                    return Bannerlist;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BannerT>> GetPortalBannersT()
        {
            var response = await httpClient.GetAsync("api/banner/GetPortalBannersT");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var Bannerlist = JsonConvert.DeserializeObject<List<BannerT>>(result);

                    return Bannerlist;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseModel> AddBannerT(RequestBannerT addBanner)
        {

            if (addBanner.File != null)
            {
                responseUpload = await UploadFile(addBanner.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }


            BannerT bannerT = new BannerT()
            {
                BannerName = addBanner.BannerName,
                Url = addBanner.Url,
                CreateDate = addBanner.CreateDate,
                StatusAnnounce = addBanner.StatusAnnounce,
                UploadImagePath = responseUpload.Path_Url,
                UploadImageName = responseUpload.FileName,
                UploadFileName = null,
                UploadFilePath = null,
                IsActive = true,
                IsStatus = 1,
                CreateBy = "",
                UpdateDate = DateTime.Now,
                UpdateBy = ""
            };

            var response = await httpClient.PostAsJsonAsync("api/banner/AddBannerT", bannerT);
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

        public async Task<ResponseModel> EditBannerT(RequestBannerT editBanner)
        {
            var response = await httpClient.PostAsJsonAsync("api/banner/EditBannerT", editBanner);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var success = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "update your record success"
                };
                return success;
            }
            var fail = new ResponseModel
            {
                issucessStatus = false,
                statusCode = "400",
                statusMessage = "update your record fail"
            };
            return fail;
        }
        public async Task<BannerT> GetBannerT(int id)
        {
            var response = await httpClient.GetAsync("api/banner/GetBannerT/" + $"{id}");
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bannerList = JsonConvert.DeserializeObject<BannerT>(result);
                return bannerList;
            }
            return null;
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
                        var response = await httpClient.PostAsync("api/banner/uploadBanner", formData);
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
