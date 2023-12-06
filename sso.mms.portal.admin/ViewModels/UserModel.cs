using System;
using sso.mms.helper.Data;

namespace sso.mms.portal.admin.ViewModels
{
    public class SsoUserRole
    {
        public SsoUserM? User { get; set; }
        public List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>? RoleListView { get; set; }
        public List<RoleUserMapping>? RoleList { get; set; }
        public string? RoleSelectedString { get; set; }
    }
    public class HosUserRole
    {
        public HospitalUserM? User { get; set; }
        public List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>? RoleListView { get; set; }
        public List<RoleUserMapping>? RoleList { get; set; }
        public string? RoleSelectedString { get; set; }
    }
    public class AuditUserRole
    {
        public AuditorUserM? User { get; set; }
        public List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>? RoleListView { get; set; }
        public List<RoleUserMapping>? RoleList { get; set; }
        public string? RoleSelectedString { get; set; }
    }
}