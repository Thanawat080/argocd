using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class AutMRoleGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string RoleCode { get; set; } = null!;

    public string? RoleDesc { get; set; }

    public bool? IsActive { get; set; }

    public string? UserGroup { get; set; }
}
