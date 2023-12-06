using sso.mms.helper.Data;

namespace sso.mms.batch.ViewModels
{
    public class BatchSyncModels
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

        public decimal? GroupId { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public decimal? SsoDeptId { get; set; }

        public decimal? SsoPositionId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserType { get; set; }

        public string? IdentificationNumber { get; set; }

        public bool? IsActive { get; set; }

    }

    public class BatchSyncHosModels
    {

        public int Id { get; set; }

        public string? PrefixMCode { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? SsoBranchCode { get; set; }

        public string? MedicalName { get; set; }

        public string? MedicalCode { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageName { get; set; }

        public int? GroupId { get; set; }

        public string? Address { get; set; }

        public int? Moo { get; set; }

        public string? DistrictCode { get; set; }

        public string? SubdistrictCode { get; set; }

        public string? ProvinceCode { get; set; }

        public string? ZipCode { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public bool? IsActive { get; set; }

        public int? HospitalMId { get; set; }

        public string? UserName { get; set; }

        public string? IdenficationNumber { get; set; }

        public bool? IsCheckDopa { get; set; }

        public string? PositionName { get; set; }
        public string? HospitalMCode { get; set; }

    }


    public class BatchSyncAuditModels
    {

        public int Id { get; set; }

        public string? PrefixMCode { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? IdenficationNumber { get; set; }

        public string? CertNo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageName { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? UserName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Position { get; set; }

    }



    public class BatchSyncMRoleGroupModels
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? RoleCode { get; set; }

        public string? RoleDesc { get; set; }

        public bool? IsActive { get; set; }

        public string? UserGroup { get; set; }

    }


    public class BatchSyncRoleUserMapping
    {
        public int RoleGroupId { get; set; }

        public string UserName { get; set; } = null!;

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? UserType { get; set; }

        public bool? IsActive { get; set; }

    }

    public class BatchSyncMPrefix
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateOnly? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateOnly? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }

    public partial class BatchSyncAutMApp
    {
        public int Id { get; set; }

        public string? AppCode { get; set; }

        public string? Name { get; set; }

        public string? SystemDesc { get; set; }

        public string? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? Url { get; set; }
    }

    public partial class BatchSyncAutMMenu
    {
        public int Id { get; set; }

        public string? MenuName { get; set; }

        public string? AppCode { get; set; }

        public string? MenuCode { get; set; }

        public string? MenuDesc { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }


    public partial class BatchSyncAutRoleMenu
    {
        public int Id { get; set; }

        public int? RoleGroupMId { get; set; }

        public int? RoleMenuMId { get; set; }

        public bool? IsRoleRead { get; set; }

        public bool? IsRoleCreate { get; set; }

        public bool? IsRoleUpdate { get; set; }

        public bool? IsRoleDelete { get; set; }

        public bool? IsRolePrint { get; set; }

        public bool? IsRoleApprove { get; set; }

        public bool? IsRoleCancel { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }


    public class CheckDopaModel
    {
        public string? UID13 { get; set; }
        public string? BOD { get; set; }
        public string? LAZER_CODE { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
    }

    public class ValidateDopaModel
    {
        public string? result { get; set; }
        public string? message { get; set; }
    }
    public class VerificationDopaModel
    {
        public string? result { get; set; }
        public string? id { get; set; }
        public string? type { get; set; }
        public string? code { get; set; }
        public string? message { get; set; }
    }


    public class ResponseDopaAuth
    {
        public string Result { get; set; }
    }

    public class BatchsyncHospitalM
    {
        public string? Code { get; set; }

        public string? Name { get; set; }


    }


}
