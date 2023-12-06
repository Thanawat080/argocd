using sso.mms.helper.Data;

namespace sso.mms.login.ViewModels.UserModels
{
    public class SsoUserProfile
    {
        public int Id { get; set; }

        public string? PrefixMCode { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageName { get; set; }

        public int? GroupId { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string UserName { get; set; } = null!;
        public virtual RoleGroupM? Group { get; set; }
    }
}
