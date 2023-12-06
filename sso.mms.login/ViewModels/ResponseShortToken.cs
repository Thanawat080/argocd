using sso.mms.helper.Data;
using sso.mms.login.ViewModels.UserModels;

namespace sso.mms.login.ViewModels
{
    public class ResponseShortToken
    {
        public string? shortToken { get; set; }
        public string? accessToken { get; set; }
        public int? expiresIn { get; set; }
        public int? refreshExpiresIn { get; set; }
        public string? refreshToken { get; set; }
        public string? tokenType { get; set; }
        public int? notBeforePolicy { get; set; }
        public string? sessionState { get; set; }
        public string? scope { get; set; }
        public string? realmGroup { get; set; }
        public string? identification_number { get; set; }
        public int? userid { get; set; }
        public int? PrefixMcode { get; set; }
        public string? UserName { get; set; }
        public int? HospitalMId { get; set;}
        public string? HospitalMCode { get; set; }
        public int? HospitalUserMId { get; set; }
        public int? SsoUserMId { get; set; }
        public int? AuditorUserMId { get; set; }

        public string? given_name { get; set; }
        public ResponseLogin? responseUser { get; set; }
    }
}
