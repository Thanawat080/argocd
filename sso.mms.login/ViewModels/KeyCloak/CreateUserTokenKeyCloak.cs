namespace sso.mms.login.ViewModels.KeyCloak
{
    public class CreateUserTokenKeyCloak
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool enabled { get; set; }
        public string username { get; set; }
        public List<Credential> credentials { get; set; }
        public Attributes attributes { get; set; }
        public class Credential
        {
            public string type { get; set; }
            public string value { get; set; }
            public bool temporary { get; set; }
        }
        public class Attributes
        {
            public string? identification_number { get; set; }
            public string? UserId { get; set; }
            public string? OrganizeId { get; set; }
        }

    }
}
