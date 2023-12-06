using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleGroupListT
{
    public int RoleGroupMId { get; set; }

    public int RoleMenuMId { get; set; }

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

    public int Id { get; set; }

    public virtual RoleGroupM RoleGroupM { get; set; } = null!;

    public virtual RoleMenuM RoleMenuM { get; set; } = null!;
}
