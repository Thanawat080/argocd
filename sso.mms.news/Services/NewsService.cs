
using Newtonsoft.Json;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Configs;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using sso.mms.news.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;

namespace sso.mms.news.Services
{
    public class NewsService
    {
        private readonly HttpClient httpClient;
        private readonly UploadFileService uploadFileService;
        public ResponseUpload? responseUpload;
        public NewsService(HttpClient httpClient, UploadFileService uploadFileService)
        {
            this.httpClient = httpClient;
            this.uploadFileService = uploadFileService;
        }

        public async Task<ResponseUpload> UploadFile(IBrowserFile file)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = file.OpenReadStream(maxAllowedSize:10240000))
                    {
                        var fileContent = new StreamContent(fileStream);
                        formData.Add(fileContent, "upload", file.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync("api/News/UploadFile", formData);

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

        public async Task<List<NewsTagListModel>> GetNewsTagList(string filter)
        {

            var response = await httpClient.GetAsync($"api/news/getNewsTag?filter={filter}");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    var newsTagLists = JsonConvert.DeserializeObject<List<NewsTagListModel>>(result);

                    return newsTagLists;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel> AddNewsService(NewTransectionRequest addNews)
        {
            // File != null 

            if (addNews.File != null)
            {
                responseUpload = await UploadFile(addNews.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }

            ResponseModel responseModel = new ResponseModel();

            try
            {
                var data = new NewTransectionRequest
                {
                    Title = addNews.Title,
                    Content = addNews.Content,
                    ImagePathM = addNews.ImagePathM,
                    ImageFileM = addNews.ImageFileM,
                    UploadPath = responseUpload.Path_Url,
                    UploadFile = responseUpload.FileName,
                    NewsMId = addNews.NewsMId,
                    NotiMId = addNews.NotiMId,
                    TagList = addNews.TagList,
                    StartDate = addNews.StartDate,
                    EndDate = addNews.EndDate,
                    PinStatus = addNews.PinStatus,
                    PrivilegePrivate = addNews.PrivilegePrivate,
                    PrivilegePublic = addNews.PrivilegePublic,
                    PrivilegeSso = addNews.PrivilegeSso,
                    IsStatus = 1,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateBy = addNews.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = addNews.UpdateBy
                };


                var response = await httpClient.PostAsJsonAsync("api/News/AddNews", data);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    responseModel.statusCode = response.StatusCode.ToString();
                    responseModel.issucessStatus = response.IsSuccessStatusCode;
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                responseModel.statusCode = "Fail";
                responseModel.issucessStatus = false;
                responseModel.statusMessage = ex.Message;
                return responseModel;
            }
        }

        public async Task<ResponseModel> EditNewsService(NewTransectionRequest requestEditNews)
        {
            if (requestEditNews.File != null)
            {
                responseUpload = await UploadFile(requestEditNews.File);
            }
            else
            {
                responseUpload = new ResponseUpload();
            }

            ResponseModel responseModel = new ResponseModel();

            try
            {
                var data = new NewTransectionRequest
                {
                    Id= requestEditNews.Id,
                    Title = requestEditNews.Title,
                    Content = requestEditNews.Content,
                    ImagePathM = requestEditNews.ImagePathM,
                    ImageFileM = requestEditNews.ImageFileM,
                    UploadPath = responseUpload.Path_Url,
                    UploadFile = responseUpload.FileName,
                    NewsMId = requestEditNews.NewsMId,
                    NotiMId = requestEditNews.NotiMId,
                    TagList = requestEditNews.TagList,
                    StartDate = requestEditNews.StartDate,
                    EndDate = requestEditNews.EndDate,
                    PinStatus = requestEditNews.PinStatus,
                    PrivilegePrivate = requestEditNews.PrivilegePrivate,
                    PrivilegePublic = requestEditNews.PrivilegePublic,
                    PrivilegeSso = requestEditNews.PrivilegeSso,
                    IsStatus = 1,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateBy = requestEditNews.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = requestEditNews.UpdateBy
                };
                var response = await httpClient.PostAsJsonAsync("api/News/EditNews/", data);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    responseModel.statusCode = response.StatusCode.ToString();
                    responseModel.issucessStatus = response.IsSuccessStatusCode;
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                responseModel.statusCode = "Fail";
                responseModel.issucessStatus = false;
                responseModel.statusMessage = ex.Message;
                return responseModel;
            }

        }

        public async Task<List<NewsMView>> GetNewsM(int newsMid)
        {
            var response = await httpClient.GetAsync("api/News/GetDataNewsM/" + newsMid);
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    var newsList = JsonConvert.DeserializeObject<List<NewsMView>>(result);

                    return newsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<NewsT> GetNews(int id)
        {
            var response = await httpClient.GetAsync("api/News/GetDataNews/" + id);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    var newsList = JsonConvert.DeserializeObject<NewsT>(result);

                    return newsList!;
                }
                return null!;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<List<NewsM>> GetGroupNewT()
        {
            var response = await httpClient.GetAsync("api/News/GetListNewsM");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var resultM = response.Content.ReadAsStringAsync().Result;

                    var newsMList = JsonConvert.DeserializeObject<List<NewsM>>(resultM);

                    return newsMList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<ResponseModel> DeleteDataService(NewsTView addNews)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/News/DeleteData/", addNews);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    responseModel.statusCode = response.StatusCode.ToString();
                    responseModel.issucessStatus = response.IsSuccessStatusCode;
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                responseModel.statusCode = "Fail";
                responseModel.issucessStatus = false;
                responseModel.statusMessage = ex.Message;
                return responseModel;
            }

        }
        public async Task<List<NewsMView>> SearchDataService(SearchModel value)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/News/Search/", value);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    var newsList = JsonConvert.DeserializeObject<List<NewsMView>>(result);

                    return newsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                responseModel.statusCode = "Fail";
                responseModel.issucessStatus = false;
                responseModel.statusMessage = ex.Message;
                return null;
            }

        }
    }
}