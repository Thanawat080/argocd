namespace sso.mms.batch.ViewModels
{
    public class KeyCloakModels
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int notbeforepolicy { get; set; }
        public string scope { get; set; }
    }



    public class SsoUserModel
    {
        public string id { get; set; }
        public long createdTimestamp { get; set; }
        public string username { get; set; }
        public bool enabled { get; set; }
        public bool totp { get; set; }
        public bool emailVerified { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string federationLink { get; set; }

        public AttributesModel attributes { get; set; }

    }

    public class AttributesModel
    {
        public List<string> OrganizeId { get; set; }
        public List<string> workingdeptdescription { get; set; }

        public List<string> ssopersonfieldposition { get; set; }
        
        public List<string> ssopersoncitizenid { get; set; }
        public List<string> identification_number { get; set; }
        public List<string> SSObranchCode { get; set; }

    }




}
