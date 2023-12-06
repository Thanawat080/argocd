using sso.mms.helper.Data;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.Email;

namespace sso.mms.login.Interface
{
    public interface IUserService
    {
        Task<dynamic> GetUserById(int id,string rleamGroup);
        Task<HospitalUserM> GetUserByEmail(string email);
        Task<ResponseChangePasswordModel> ChangePassword(string newPassword, string userName, string oldPassword, int userId, string realmGroup);
        Task<ResponseModel> VerifyOtp(VerifyOTP verifyOTP);
        Task<ResponseModel> SendMailAsync(RequestEmail requestEmail);
        Task<ResponseModel> SendOtpToMail(RequestOtpEmail requestOtp);
        Task<ResponseModel> CheckDopa(CheckDopaModel dopaData);
        Task<List<PrefixM>> getPrefix();

    }
}
