using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleUserMapping
{
    public int RoleGroupId { get; set; }

    public string UserName { get; set; } = null!;

    public int IsStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string? UserType { get; set; }

    public bool? IsActive { get; set; }

    public virtual RoleGroupM RoleGroup { get; set; } = null!;
}
