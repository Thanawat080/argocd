using System.Collections;

namespace sso.mms.portal.admin.ViewModels
{
    public class RoleGroupMdata
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsActive { get; set; } 

        public int IsStatus { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } 

        public DateTime UpdateDate { get; set; }

        public string UpdateBy { get; set; } 
    }
}
