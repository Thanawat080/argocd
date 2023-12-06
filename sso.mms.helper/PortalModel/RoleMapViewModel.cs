namespace sso.mms.helper.PortalModel
{
    public class RoleMapViewModel
    {
        public string username { get; set; }
        public string roleCode { get; set; }
        public string menuCode { get; set; }
        public string appCode { get; set; }
        public bool? isRoleRead { get; set; }
        public bool? isRoleCreate { get; set; }
        public bool? isRoleUpdate { get; set; }
        public bool? isRoleDelete { get; set; }
        public bool? isRolePrint { get; set; }
        public bool? isRoleApprove { get; set; }
        public bool? isRoleCancle { get; set; }
    }

    public class UserRole
    {
        public string username { get; set; }
        public List<RoleList> role { get; set; }
    }

    public class RoleList
    {
        public string roleCode { get; set; }
        public List<MenuPerMit> menu { get; set; }
    }
    public class MenuPerMit
    {
        public string menuCode { get; set; }
        public string appCode { get; set; }
        public bool? isRoleRead { get; set; }
        public bool? isRoleCreate { get; set; }
        public bool? isRoleUpdate { get; set; }
        public bool? isRoleDelete { get; set; }
        public bool? isRolePrint { get; set; }
        public bool? isRoleApprove { get; set; }
        public bool? isRoleCancle { get; set; }
    }
}
