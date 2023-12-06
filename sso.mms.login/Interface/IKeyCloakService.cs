using Microsoft.AspNetCore.Mvc;
using sso.mms.helper.Data;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels.KeyCloak;

namespace sso.mms.login.Interface
{
    public interface IKeyCloakService
    {

        Task<ResponseLoginTokenKeyCloak> LoginKeyCloak(string username, string password, string realmGroup);
        Task<ResponseCreateTokenKeyCloak> CreateTokenKeyCloak(string realmGroup);
        Task<ResponseModel> CreateUserKeyCloak(HospitalUserM? hospitalUser,AuditorUserM? auditorUser, string token, string realmGroup);
        Task<ActionResult<ResponseLoginTokenKeyCloak>> RefreshToken(string refreshToken, string realmGroup);

        Task<ResponseModel> CheckUserName(string username);
    }
}
