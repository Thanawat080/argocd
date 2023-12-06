using sso.mms.login.ViewModels.UserModels;

namespace sso.mms.login.Interface
{
    public interface IUtilService
    {
        Task<HospitalUserProfile> HospitalUserProfile(int hospitalMId);
        Task<List<HospitalUserProfile>> HospitalUserProfileList();
        Task<AuditorUserProfile> AuditorUserProfile(int auditorId);
        Task<List<AuditorUserProfile>> AuditorUserProfileList();
        Task<SsoUserProfile> SsoUserProfile(int ssoUserId);
        Task<List<SsoUserProfile>> SsoUserProfileList();

    }
}
