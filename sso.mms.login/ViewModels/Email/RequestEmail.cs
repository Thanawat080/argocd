
namespace sso.mms.login.ViewModels.Email
{
    public class RequestEmail
    {
        public string ToEmail { get; set; }
        public string SubjectEmail { get; set; }

        public string? LinkForgotPassword { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
