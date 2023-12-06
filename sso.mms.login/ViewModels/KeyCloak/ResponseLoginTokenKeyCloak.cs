﻿namespace sso.mms.login.ViewModels.KeyCloak
{
    public class ResponseLoginTokenKeyCloak
    {
        public string? access_token { get; set; }
        public int? expires_in { get; set; }
        public int? refresh_expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public int? notbeforepolicy { get; set; }
        public string? session_state { get; set; }
        public string? scope { get; set; }
        public string? shortToken { get; set; }
        public string? realmGroup { get; set; }

        public string? identfication_number { get; set; }

        public ResponseLogin? responseUser { get; set; }
        

    }
}
