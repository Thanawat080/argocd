namespace sso.mms.login.ViewModels
{
    public class  ValidateDopaModel
    {
        public string? result { get; set; }
        public string? message { get; set; }
    }
    public class VerificationDopaModel
    {
        public string? result { get; set; }
        public string? id { get; set; }
        public string? type { get; set; }
        public string? code { get; set; }
        public string? message { get; set; }
    }
    public class ResponseDopaAuth
    {
        public string Result { get; set; }
    }
}

