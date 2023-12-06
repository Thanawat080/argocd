using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.login.ViewModels;
using System.Net.Mail;
using System.Net;
using Humanizer;
using sso.mms.login.ViewModels.Email;
using System;
using sso.mms.helper.ViewModels;
using System.Net.Http;
using System.Reflection.Metadata;
using sso.mms.helper.Utility;
using sso.mms.login.ViewModels.KeyCloak;
using System.Net.Http.Headers;
using System.Net.Sockets;
using sso.mms.login.Services;
using sso.mms.login.ViewModels.User;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Linq.Expressions;
using sso.mms.login.ViewModels.UserModels;
using sso.mms.login.ViewModels.Parameter;
using sso.mms.login.Interface;
using sso.mms.helper.Services;
using sso.mms.helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IdpDbContext idpDbContext;
        private readonly HttpClient httpClient;
        private readonly KeyCloakService keyCloakService;

        public string? SecretKey = ConfigureCore.SecretKey;
        private readonly IUtilService utilService;
        public UserController(IdpDbContext idpDbContext, IUtilService utilService, HttpClient httpClient, KeyCloakService keyCloakService)
        {
            this.idpDbContext = idpDbContext;
            this.httpClient = httpClient;
            this.keyCloakService = keyCloakService;
            this.utilService = utilService;
        }
        private string CheckSecretRealmGroup(string realmGroup)
        {
            switch (realmGroup)
            {
                case "sso-mms-hospital":
                    return ConfigureCore.secretKeyRealmGroupHospital;

                case "sso-mms-admin":
                    return ConfigureCore.secretKeyRealmGroupAdmin;

                case "sso-mms-audit":
                    return ConfigureCore.secretKeyRealmGroupAuditor;
            }
            return "";
        }

        // GET: api/<UserController>

        [HttpGet("[action]")]
        public async Task<ActionResult<dynamic>> GetUserById(int id,string rleamGroup)
        {
            dynamic userById;
            try
            {

           
            if (rleamGroup == "sso-mms-auditor")
            {
                var getuser = await idpDbContext.AuditorUserMs.FirstOrDefaultAsync(e => e.Id == id);
                userById = getuser;
            }
            else
            {
                var getuser = await idpDbContext.HospitalUserMs.FirstOrDefaultAsync(e => e.Id == id);
                userById = getuser;
            }

          
            return userById;  
            } catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("GetUser")]
        public async Task<IEnumerable<HospitalUserM>> GetUser()
        {
            return await idpDbContext.HospitalUserMs.Select(e => new HospitalUserM
            {
                Id = e.Id,
                Mobile = e.Mobile,
                IdenficationNumber = e.IdenficationNumber,
                MedicalCode = e.MedicalCode,

            }).OrderByDescending(e => e.Id).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<HospitalUserM>> GetUserByEmail(string email)
        {
            var userHosByEmail = await idpDbContext.HospitalUserMs.FirstOrDefaultAsync(e => e.Email == email);
            //var userAuditorByEmail = await idpDbContext.AuditorUserMs.FirstOrDefaultAsync(e => e.Email == email);
            //var userSSOByEmail = await idpDbContext.SsoUserMs.FirstOrDefaultAsync(e => e.Email == email);

            if (userHosByEmail != null)
            {
                return userHosByEmail;
            }
            else
            {
                return BadRequest();

            }

        }
        [HttpPost("[action]")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel value)
        {
            try
            {
                var encryptedString = AesOperation.EncryptString(SecretKey, value.newpassword);
                var userdata = await idpDbContext.HospitalUserMs.FindAsync(value.userid);
                if (userdata != null)
                {
                    userdata.Id = value.userid;
                    userdata.UpdateDate = DateTime.Now;
                    userdata.UpdateBy = value.userid.ToString();
                    userdata.Password = encryptedString;
                    //userdata.Password = value.newpassword;
                }
                //idpDbContext.HospitalUserMs.Update(userdata);
                await idpDbContext.SaveChangesAsync();

                return Ok(new { StatusCode = "true" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = "false", message = ex.Message });
            }
        }


        [HttpPost("sendOtpToMail")]
        public async Task<ActionResult> SendOtpToMail(RequestOtpEmail requestOtp)
        {

            // Create a new SMTP client

            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();

            var request = new SsoOtpT
            {
                OtpCode = OTP,
                Email = requestOtp.ToEmail,
                Mobile = requestOtp.Mobile,
                OtpType = requestOtp.OTP_Type,
                RealmGroup = requestOtp.RealmGroup,
                CreateBy = requestOtp.UserName,
                CreateDate = DateTime.Now,
                UpdateBy = requestOtp.UserName,
                UpdateDate = DateTime.Now,

            };
            await idpDbContext.SsoOtpTs.AddAsync(request);
            await idpDbContext.SaveChangesAsync();

            if (ConfigureCore.SiteName == "SSO")
            {
                var result = await EmailUatService.SSOSendEmail(requestOtp.SubjectEmail!, request.OtpCode, requestOtp.ToEmail!, null, null);
                return Ok(new ResponseModel { statusCode = result.statusCode, issucessStatus = result.issucessStatus, statusMessage = result.statusMessage });
            }
            else {
                var result = await EmailUatService.BTSendEmail(requestOtp.SubjectEmail!, request.OtpCode, requestOtp.ToEmail!, null, null);
                return Ok(new ResponseModel { statusCode = result.statusCode, issucessStatus = result.issucessStatus, statusMessage = result.statusMessage });
            }
        }


        [HttpPost("sendMail")]
        public async Task<ActionResult> SendMailAsync(RequestEmail requestEmail)
        {
            if (ConfigureCore.SiteName == "SSO")
            {
                var result = await EmailUatService.SSOSendEmail(requestEmail.SubjectEmail, null, requestEmail.ToEmail, requestEmail.Username, requestEmail.Password);
                return Ok(new ResponseModel { statusCode = result.statusCode, issucessStatus = result.issucessStatus, statusMessage = result.statusMessage });
            }
            else {
                var result = await EmailUatService.BTSendEmail(requestEmail.SubjectEmail, null, requestEmail.ToEmail, requestEmail.Username, requestEmail.Password);
                return Ok(new ResponseModel { statusCode = result.statusCode, issucessStatus = result.issucessStatus, statusMessage = result.statusMessage });
            }
        }

        [HttpPost("sendVerifyOTPMail")]
        public async Task<ActionResult> SendVerifyOtpAsync(VerifyOTP verifyOTP)
        {

            var query = idpDbContext.SsoOtpTs.Where(w => w.OtpCode == verifyOTP.OtpCode).Select(t => new SsoOtpT
            {
                Id = t.Id,
                OtpCode = t.OtpCode,
                CreateBy = t.CreateBy,
                CreateDate = t.CreateDate,
                UpdateBy = t.UpdateBy,
                UpdateDate = t.UpdateDate,
            }).FirstOrDefaultAsync();

            if (query.Result == null)
            {
                return Ok(new ResponseModel { statusCode = "400", issucessStatus = false, statusMessage = "wrong otp" });
            }

            var otpTime = query.Result?.CreateDate.Value.Minute;
            var currentTime = DateTime.Now.Minute;
            var timeOut = currentTime - otpTime;

            if (timeOut > 5)
            {
                return Ok(new ResponseModel { statusCode = "400", issucessStatus = false, statusMessage = "your timeout accept otp" });
            }
            else
            {
                return Ok(new ResponseModel { statusCode = "200", issucessStatus = true, statusMessage = "your success accept otp" });
            }
        }


        [HttpGet("[action]")]
        public async Task<List<PrefixM>> getPrefix()
        {
            return await idpDbContext.PrefixMs.Where(w => w.IsActive == true).ToListAsync();

        }

        [HttpPost("deleteUserHospital")]
        public async Task<ActionResult> DeleteUserHospital(RequestUserHospitalMId requestUserHospitalMId)
        {
            var hospitalUserMbyId = await idpDbContext.HospitalUserMs.Where(x => x.Id == requestUserHospitalMId.Id).FirstOrDefaultAsync();

            if (hospitalUserMbyId != null)
            {
                idpDbContext.HospitalUserMs.Remove(hospitalUserMbyId);
                await idpDbContext.SaveChangesAsync();

                return Ok(new ResponseModel { issucessStatus = false, statusCode = "200", statusMessage = $"Delete User: {hospitalUserMbyId.Id} Success" });
            }
            else
            {
                return Ok(new ResponseModel { issucessStatus = false, statusCode = "400", statusMessage = "Not Found User" });
            }

        }

        [HttpGet("userRealmById")]
        public async Task<ActionResult<dynamic>> GetUserRealmById([FromQuery] QueryStringUser queryString)
        {
            switch (queryString.realmGroup)
            {
                case "sso-mms-hospital":
                    var getHospitalUserProfile = await utilService.HospitalUserProfile(queryString.userId);

                    var responseHosProfile = new ResponseResult<HospitalUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        Result = getHospitalUserProfile,
                    };

                    return responseHosProfile;

                case "sso-mms-admin":
                    var ssoUserProfile = await utilService.SsoUserProfile(queryString.userId);

                    var responseSsoProfile = new ResponseResult<SsoUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        Result = ssoUserProfile,
                    };

                    return responseSsoProfile;
                default:
                    var auditorUser = await utilService.AuditorUserProfile(queryString.userId);

                    var responseAuditorProfile = new ResponseResult<AuditorUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        Result = auditorUser,
                    };
                    return responseAuditorProfile;
            }
        }

        [HttpGet("userListByRealm")]
        public async Task<ActionResult<dynamic>> GetUserListByRealm([FromQuery] string realmGroup)
        {
            switch (realmGroup)
            {
                case "sso-mms-hospital":
                    var getHospitalUserProfile = await utilService.HospitalUserProfileList();

                    var responseHosProfile = new ResponseResultList<HospitalUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        ResultList = getHospitalUserProfile,
                    };

                    return responseHosProfile;

                case "sso-mms-admin":
                    var ssoUserProfile = await utilService.SsoUserProfileList();

                    var responseSsoProfile = new ResponseResultList<SsoUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        ResultList = ssoUserProfile,
                    };

                    return responseSsoProfile;
                default:
                    var auditorUser = await utilService.AuditorUserProfileList();

                    var responseAuditorProfile = new ResponseResultList<AuditorUserProfile>()
                    {
                        StatusCode = 200,
                        Status = true,
                        ResultList = auditorUser,
                    };
                    return responseAuditorProfile;
            }
        }
    }
}
