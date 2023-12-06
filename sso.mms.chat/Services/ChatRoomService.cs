using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using Newtonsoft.Json;
using sso.mms.chat.ViewModels;
using sso.mms.helper.Data;
using AntDesign;
namespace sso.mms.chat.Services
{
    public class ChatRoomService
    {
        private readonly HttpClient httpClient;

        public string? env = ConfigureCore.ConfigENV;
        private string prefix = "";


        public ChatRoomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ChatRoom>> GetChatRoom(int? hospitalId,string? username)
        {
            var response = await httpClient.GetAsync($"api/chatroom/getChatRoom?hospitalId={hospitalId}&username={username}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatRoom>>(result);
                return res;
            }
            return null;
        }

        public async Task<List<ChatRoomMListModel>> GetChatRoomMaster(string username)
        {
            var response = await httpClient.GetAsync($"api/chatroom/getChatRoomMaster?username={username}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var res = JsonConvert.DeserializeObject<List<ChatRoomMListModel>>(result);
                return res;
            }
            return null;
        }

        public async Task<ResponseModel> CreateChatRoom(ChatRoom chatRoom)
        {

            ResponseModel responseModel = new ResponseModel();

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/chatroom/CreateChatRoom", chatRoom);
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
    }
}
