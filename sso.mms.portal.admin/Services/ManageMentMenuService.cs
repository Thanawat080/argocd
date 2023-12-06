using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;
using sso.mms.portal.admin.Pages.EditBanner;


namespace sso.mms.portal.admin.Services
{
    public class ManageMentMenuService
    {
        private readonly HttpClient httpClient;

        public string? env = ConfigureCore.ConfigENV;
        public ResponseUpload responseUpload;
        private string prefix = "";
        public ManageMentMenuService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
        }

        public async Task<List<RoleAppM>> GetManageMentmenu()
        {
            var response = await httpClient.GetAsync("api/managementmenu/getmenu");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleAppM>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RoleMenuM>> GetManageMentmenuEdit(string appcode)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getmenueditbycode/" + $"{appcode}");
            response.EnsureSuccessStatusCode();
            try
            {

                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleMenuM>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RoleAppM> GetAppById(int id)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getappmenu/" + $"{id}");
            response.EnsureSuccessStatusCode();
            try
            {

                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<RoleAppM>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<string>  UpdateApp(RoleAppM data)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/managementmenu/updateApp", data);
                response.EnsureSuccessStatusCode();
                var response1 = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncMApp");
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<SsoUserM>> GetUserSso(string? text = null , int? length = null)
        {
            var response = await httpClient.GetAsync($"api/managementmenu/getUserbyTypeSSO?text={text}&length={length}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<SsoUserM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<HospitalUserM>> GetUserHos()
        {
            var response = await httpClient.GetAsync("api/managementmenu/getUserbyTypeHos");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<HospitalUserM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<AuditorUserM>> GetUserAudit()
        {
            var response = await httpClient.GetAsync("api/managementmenu/getUserbyTypeAudit");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<AuditorUserM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<RoleGroupM>> GetRoleGroupByType(string type)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getRoleGroupByType/" + $"{type}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleGroupM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<string> saveAddRole(List<ManageMentMenuModel.ViewModelForSaveGroupList> data, RoleGroupM rolegroupm)
        {

            try
            {
                var checkdupRoleCode = await httpClient.GetAsync("api/managementmenu/checkrolecode/"+ $"{rolegroupm.RoleCode}");
                var resultcheckdupRoleCode = checkdupRoleCode.Content.ReadAsStringAsync().Result;
                var LogListresultcheckdupRoleCode = JsonConvert.DeserializeObject<List<RoleGroupM>>(resultcheckdupRoleCode);

                if (LogListresultcheckdupRoleCode.Count() == 0)
                {
                    var response1 = await httpClient.PostAsJsonAsync("api/managementmenu/insertRoleGrouplist", rolegroupm);
                    response1.EnsureSuccessStatusCode();
                    var response11 = response1.Content.ReadAsStringAsync().Result;
                    //response1.
                    foreach (var item in data)
                    {
                        item.RoleGroupMId = Int32.Parse(response11);
                    }

                    //rolegrouplist
                    var response = await httpClient.PostAsJsonAsync("api/managementmenu/addRoleGroup", data);
                    response.EnsureSuccessStatusCode();
                    var responsesyncMMenu = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleMenu");
                    var responsesyncMRoleGroup = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncMRoleGroup");
                    return "success";
                }
                else {
                    return "duplicaterolecode";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<RoleGroupM> getRoleGroupById(int id)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getRoleGroupById/" + $"{id}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<RoleGroupM>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<ManageMentMenuModel.ViewModelForSaveGroupList>> getRoleGroupListByRoleGroupMId(int id)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getRoleGroupListByRoleGroupMId/" + $"{id}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<ManageMentMenuModel.ViewModelForSaveGroupList>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> editAddRole(List<ManageMentMenuModel.ViewModelForSaveGroupList> data, RoleGroupM rolegroupm, List<int> listIdFordelete)
        {

            try
            {
                if (listIdFordelete.Count > 0) {
                    foreach (var item in listIdFordelete)
                    {
                        var response2 = await httpClient.GetAsync("api/managementmenu/delteRoleGroupListT/" + $"{item}");
                        response2.EnsureSuccessStatusCode();
                    }
                    
                }
                var response1 = await httpClient.PostAsJsonAsync("api/managementmenu/editRoleGrouplist", rolegroupm);
                response1.EnsureSuccessStatusCode();

                foreach (var item in data)
                {
                    if (!item.update) {
                        item.RoleGroupMId = rolegroupm.Id;
                    }
                    
                }

                //rolegrouplist
                var response = await httpClient.PostAsJsonAsync("api/managementmenu/addRoleGroup", data);
                response.EnsureSuccessStatusCode();
                var responsesyncMMenu = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleMenu");
                var responsesyncMRoleGroup = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncMRoleGroup");
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<string> delteRoleGruopM(int id)
        {

            try
            {
                var response = await httpClient.GetAsync("api/managementmenu/deleteRoleGroupM/" + $"{id}");
                response.EnsureSuccessStatusCode();
                var responsesyncMRoleGroup = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncMRoleGroup");
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
                else {
                    return "dontsuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>> getRoleUserMappingView(string userName)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getRoleUserMappingView/" + $"{userName}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RoleUserMapping>> getRoleUserMapping(string userName)
        {
            var response = await httpClient.GetAsync("api/managementmenu/getRoleUserMapping/" + $"{userName}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleUserMapping>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> saveRoleUserMapping(ManageMentMenuModel.ViewModelRoleGroup data)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/managementmenu/insertRoleUserMapping", data);
                response.EnsureSuccessStatusCode();
                var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleUserMapping");
                return "insert success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> deleteRoleUserMapping(string userName)
        {
            try
            {
                var response = await httpClient.GetAsync("api/managementmenu/deleteRoleUserMapping/" + $"{userName}");
                response.EnsureSuccessStatusCode();
                var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleUserMapping");
                return "delete success";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
