using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleMenuM
{
    public int Id { get; set; }

    public string MenuName { get; set; } = null!;

    public int IsStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? AppCode { get; set; }

    public string? MenuCode { get; set; }

    public string? MenuDesc { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RoleGroupListT> RoleGroupListTs { get; set; } = new List<RoleGroupListT>();
}
