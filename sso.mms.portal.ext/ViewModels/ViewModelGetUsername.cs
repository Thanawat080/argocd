using sso.mms.helper.Data;

namespace sso.mms.portal.ext.ViewModels
{
    public class ViewModelGetUsername
    {
        public string? username { get; set; }
    }


    public class ViewModelMapRoleUserMApping
    {
        public int RoleGroupId { get; set; }

        public string UserName { get; set; } = null!;


        public string CreateBy { get; set; } = null!;

        public string UpdateBy { get; set; } = null!;

        public string? UserType { get; set; }

        public bool? IsActive { get; set; }
    }


    public class ViewModelHospitalCode
    {
        public int hospitalcode { get; set; }

    }

}
