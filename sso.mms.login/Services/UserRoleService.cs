using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.login.Interface;

namespace sso.mms.login.Services
{
    public class UserRoleService : IUserRoleService
    {
 
        private readonly HttpClient httpClient;
        public UserRoleService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<UserRole> GetRoleByUserName(string username)
        {
            try
            {
                var response = await httpClient.GetAsync("api/userrole/getuserrole/" + username);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    
                    var convertRes = JsonConvert.DeserializeObject<UserRole>(result.Result);

                    return convertRes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<string> GetHospitalCode(string username)
        {
            try
            {
                var response = await httpClient.GetAsync("api/userrole/getorgcode/" + username);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var convertRes = JsonConvert.DeserializeObject<string>(result.Result);

                    return convertRes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<MenuPerMit> GetUserAuth(UserRole userRole, string menuCode, string appCode)
        {
            try
            {
                List<MenuPerMit> listPermit = userRole.role.Select(i => i.menu.Where(m => m.menuCode == menuCode && m.appCode == appCode).
                Select(t => t).ToList()).ToList().SelectMany(list => list).ToList();
                MenuPerMit permit = new MenuPerMit
                {
                    isRoleRead = listPermit.Sum(x => (bool?)x.isRoleRead == true ? 1 : 0) >= 1,
                    isRoleCreate = listPermit.Sum(x => (bool?)x.isRoleCreate == true ? 1 : 0) >= 1,
                    isRoleUpdate = listPermit.Sum(x => (bool?)x.isRoleUpdate == true ? 1 : 0) >= 1,
                    isRoleDelete = listPermit.Sum(x => (bool?)x.isRoleDelete == true ? 1 : 0) >= 1,
                    isRolePrint = listPermit.Sum(x => (bool?)x.isRolePrint == true ? 1 : 0) >= 1,
                    isRoleApprove = listPermit.Sum(x => (bool?)x.isRoleApprove == true ? 1 : 0) >= 1,
                    isRoleCancle = listPermit.Sum(x => (bool?)x.isRoleCancle == true ? 1 : 0) >= 1
                };
                return permit;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<string> insertRoleUserMapping(RoleUserMappingModel data)
        {
            try
            {
                Console.WriteLine(httpClient);
                var response = await httpClient.PostAsJsonAsync("api/userrole/insertRoleUserMapping", data);
                response.EnsureSuccessStatusCode();
                var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleUserMapping");
                return "insert success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
