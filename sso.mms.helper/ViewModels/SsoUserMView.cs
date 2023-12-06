using sso.mms.helper.Data;

namespace sso.mms.helper.ViewModels
{
    public partial class SsoUserMView
    {
        public int Id { get; set; }

        public string? PrefixMCode { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? SsoBranchCode { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageName { get; set; }

        public int? GroupId { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public int? SsoDeptId { get; set; }

        public int? SsoPositionId { get; set; }

        public string UserName { get; set; }

        public string? UserType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }


        public  string? PosName { get; set; }
        public string? DepName{ get; set; }
    
  
    }
    
}
