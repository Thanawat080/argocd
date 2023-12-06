using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using sso.mms.portal.admin.ViewModels;

namespace sso.mms.portal.admin.Services
{
    public class AdminService
    {
        private readonly HttpClient httpClient;

        public ResponseUpload responseUpload;

        public AdminService(HttpClient httpClient)
        {


            this.httpClient = httpClient;
        }
        public async Task<List<ViewModelSessionUserT>> GetSessionUserT()
        {
            var response = await httpClient.GetAsync("api/Admin/GetSessionUserT");
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var LogList = JsonConvert.DeserializeObject<List<ViewModelSessionUserT>>(result);

                    return LogList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetSsoUser(string realmGroup)
        {
            Response<AuditorUserM> responseAudit = new Response<AuditorUserM>();
            Response<SsoUserM> responseSso = new Response<SsoUserM>();
            Response<dynamic> response = new Response<dynamic>();

            try
            {
                if (realmGroup == "sso-mms-auditor")
                {
                    var getSsoUser = await httpClient.GetFromJsonAsync<List<AuditorUserM>>($"api/Admin/getSsoUser?realmGroup={realmGroup}");
                    
                        responseAudit = new Response<AuditorUserM>
                        {
                            StatusCode = 200,
                            Message = "success",
                            ResultList = getSsoUser
                        };
                   
                    return responseAudit;
                }
                else
                {
                    var getSsoUser = await httpClient.GetFromJsonAsync<List<SsoUserM>>($"api/Admin/getSsoUser?realmGroup={realmGroup}");
                    responseSso = new Response<SsoUserM>
                    {
                        StatusCode = 200,
                        Message = "success",
                        ResultList = getSsoUser
                    };
                    return responseSso;
                }
                
            }catch (Exception ex)
            {
                response = new Response<dynamic>
                {
                    StatusCode = 400,
                    Message = "Error : " + ex.Message,
                  
                };
                return response;
            }
        }
        public async Task<Response<PositionM>> GetSsoPosition(int? id)
        {
            Response<PositionM> response = new Response<PositionM>();
            try
            {
                var getSsoPosition = await httpClient.GetFromJsonAsync<List<PositionM>>($"api/Admin/getSsoPosition");
                var result = getSsoPosition.FirstOrDefault(f => f.Id == id);
                response = new Response<PositionM>
                {
                    StatusCode = 200,
                    Message = "success",
                    Result = result,
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new Response<PositionM>
                {
                    StatusCode = 400,
                    Message = "Error : " + ex.Message,

                };
                return response;
            }


        }
        public async Task<Response<DepartmentM>> GetSsoDepartMent(int? id)
        {
            Response<DepartmentM> response = new Response<DepartmentM>();
            try
            {
                var getSsoPosition = await httpClient.GetFromJsonAsync<List<DepartmentM>>($"api/Admin/getSsoDepartment");
                var result = getSsoPosition.FirstOrDefault(f => f.Id == id);
                response = new Response<DepartmentM>
                {
                    StatusCode = 200,
                    Message = "success",
                    Result = result,
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new Response<DepartmentM>
                {
                    StatusCode = 400,
                    Message = "Error : " + ex.Message,

                };
                return response;
            }


        }
    }
}
