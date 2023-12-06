using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleMenuMapping
{
    public int UserId { get; set; }

    public int RoleGroupId { get; set; }

    public string? UserName { get; set; }

    public string? UserGroup { get; set; }

    public virtual RoleGroupM RoleGroup { get; set; } = null!;
}
