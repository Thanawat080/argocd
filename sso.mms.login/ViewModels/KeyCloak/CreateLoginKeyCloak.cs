namespace sso.mms.login.ViewModels.KeyCloak
{
    public class CreateLoginKeyCloak
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? access_token { get; set; }
        public int? expires_in { get; set; }
        public int? refresh_expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public string? idp_token { get; set; }
        public string? realmGroup { get; set; }

        public string? identification_number { get; set; }
        public int? userId { get; set; }
        public string? ChannelLogin { get; set; }

    }

    public class TokenKeyCLoak
    {
        public string? access_token { get; set; }
        public int? expires_in { get; set; }
        public int? refresh_expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public int? not_before_policy { get; set; }
        public string? session_state { get; set; }
        public string? scope { get; set; }

        public string? identification_number { get; set; }
        public string? realmGroup { get; set; }
    }
}
