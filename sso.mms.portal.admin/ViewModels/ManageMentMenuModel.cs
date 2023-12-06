using sso.mms.helper.Data;

namespace sso.mms.portal.admin.ViewModels
{
    public class ManageMentMenuModel
    {
        public class ViewModelSearchUser
        {
            public string? SearchText { get; set; }
            public int? maxLength { get; set; }
        }
        public class ViewModelMenu
        {
            public int Id { get; set; }
            public int Code { get; set; }
            public string Name { get; set; }
            public string url { get; set; }
            public int status { get; set; }
        }

        public class ViewModelForSaveGroupList
        {
            public int Id { get; set; } = 0;
            public int RoleGroupMId { get; set; }
            public int RoleMenuMId { get; set; }

            public bool? IsRoleRead { get; set; }

            public bool? IsRoleCreate { get; set; }

            public bool? IsRoleUpdate { get; set; }

            public bool? IsRoleDelete { get; set; }

            public bool? IsRolePrint { get; set; }

            public bool? IsRoleApprove { get; set; }

            public bool? IsRoleCancel { get; set; }

            public string Name { get; set; }

            public bool update { get; set; } = false;

            public string AppCode { get; set; }

            public bool? IsRoleAll { get; set; }


        }

        public class ViewModelForGetRoleUserMappingAndName : RoleUserMapping
        {
            public string? Name { get; set; }
        }


        public class ViewModelRoleGroup
        {

            public int RoleGroupId { get; set; }

            public string UserName { get; set; } = null!;

            public string CreateBy { get; set; } = null!;

            public string UpdateBy { get; set; } = null!;

            public string? UserType { get; set; }
        }



    }
}
