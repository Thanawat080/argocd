namespace sso.mms.login.ViewModels.KeyCloak
{
    public class CreateTokenKeyCloak
    {
        public string? grant_type { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
    }
}
