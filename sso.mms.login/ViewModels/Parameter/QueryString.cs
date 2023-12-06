using Microsoft.AspNetCore.Mvc;

namespace sso.mms.login.ViewModels.Parameter
{
    public class QueryStringUser
    {
        public int userId { get; set; }
        public string realmGroup { get; set; }
    }
}
