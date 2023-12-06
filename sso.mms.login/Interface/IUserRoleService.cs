using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;

namespace sso.mms.login.Interface
{
    public interface IUserRoleService
    {
        Task<UserRole> GetRoleByUserName(string username);
        Task<string> GetHospitalCode(string username);
        Task<MenuPerMit> GetUserAuth(UserRole userRole, string menuCode, string appCode);
        Task<string> insertRoleUserMapping(RoleUserMappingModel data);
    }
}
