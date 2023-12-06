using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sso.mms.chat.Pages;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using System.Net.Http.Headers;


namespace sso.mms.chat.Services
{
    public class ChatService
    {
        private readonly HttpClient httpClient;
        private readonly UploadFileService uploadFileService;

        public string? env = ConfigureCore.ConfigENV;
        private string prefix = "";

        public ChatService(HttpClient httpClient, UploadFileService uploadFileService)
        {
            this.httpClient = httpClient;
            this.uploadFileService = uploadFileService;
        }

        public async Task<List<ChatT>> GetChatByRoomID(int roomId)
        {

            var response = await httpClient.GetAsync("api/chat/ByRoomId/" + roomId);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatT>>(result);
                return res;
            }
            return null;
        }
        public async Task<List<ChatT>> GetChatRoomByRole(int roomId)
        {

            var response = await httpClient.GetAsync("api/chat/ByRoomId/" + roomId);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatT>>(result);
                return res;
            }
            return null;
        }

        public async Task<List<ChatT>> GetChatHistory(int roomId, int pageNumber, int pageSize)
        {

            var response = await httpClient.GetAsync($"api/chat/getChatHistory?roomId={roomId}&pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatT>>(result);
                return res;
            }
            return null;
        }
        public async Task<List<ChatT>> GetChatRoom(int roomId)
        {
            var response = await httpClient.GetAsync("api/chat/ByRoomId/" + roomId);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatT>>(result);
                return res;
            }
            return null;
        }
        public async Task<List<ChatT>> GetChat()
        {
            var response = await httpClient.GetAsync("api/chat/GetChatFromLog/");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatT>>(result);
                return res;
            }
            return null;
        }
        public async Task<List<ChatRoom>> GetChatNotiFromLog(int? hosMId)
        {
            var response = await httpClient.GetAsync($"api/chat/GetChatNotiFromLog?hosMId={hosMId}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatRoom>>(result);
                return res;
            }
            return null;
        }
        public async Task<string> UpdateChatReadStatusNoti(List<ChatLog> chat)
        { 
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/chat/UpdateChatReadStatusNoti", chat);

                if (response.IsSuccessStatusCode)
                {
                    return "Ok";
                }
                else
                {
                    return "Fail";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        
        }
            public async Task<string> UpdateChatRead(List<ChatLog> chat)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/chat/UpdateChatReadStatus", chat);

                if (response.IsSuccessStatusCode)
                {
                    return "Ok";
                }
                else
                {
                    return "Api Fail";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<ResponseModel> AddChatTransaction(ChatT addChat)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/chat/ChatMessage", addChat);
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
                        var response = await httpClient.PostAsync("api/Chat/UploadFile", formData);

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
    }
}

