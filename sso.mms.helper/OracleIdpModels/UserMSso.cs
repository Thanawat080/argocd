using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class UserMSso
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
