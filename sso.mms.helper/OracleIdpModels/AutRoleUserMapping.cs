using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class AutRoleUserMapping
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
