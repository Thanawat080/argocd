namespace sso.mms.portal.admin.ViewModels
{
    public class ResponseShortToken
    {
        public string shortToken { get; set; }
        public string accessToken { get; set; }
        public int expiresIn { get; set; }
        public int refreshExpiresIn { get; set; }
        public string refreshToken { get; set; }
        public string tokenType { get; set; }
        public int notBeforePolicy  { get; set; }
        public string sessionState { get; set; }
        public string scope { get; set; }
        public string realmGroup { get; set; }
        

    }
}
