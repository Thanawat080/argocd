namespace sso.mms.batch.ViewModels
{
    public class UserKeycloakModels
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }

        public string? username { get; set; }

        public string? email { get; set; }

        public attributes? attributes { get; set; }


    }

    public class attributes
    {
        public List<string>? SSObranchCode { get; set; }
        public List<string>? identification_number { get; set; }


    }
}
