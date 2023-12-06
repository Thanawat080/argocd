namespace sso.mms.helper.ViewModels
{
    public class RoleUserMappingModel
    {
        public int RoleGroupId { get; set; }

        public string UserName { get; set; } = null!;

        public string CreateBy { get; set; } = null!;

        public string UpdateBy { get; set; } = null!;

        public string? UserType { get; set; }
        
    }
}
