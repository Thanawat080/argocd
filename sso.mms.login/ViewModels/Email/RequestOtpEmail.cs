namespace sso.mms.login.ViewModels.Email
{
    public class RequestOtpEmail
    {
        public string? ToEmail { get; set; }
        public string? SubjectEmail { get; set; }
        public string? UserName { get; set; }
        public string? RealmGroup { get; set; }
        public string? OTP_Type { get; set; }
        public string? Mobile { get; set; }

    }
}
