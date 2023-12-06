using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleGroupM
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IsStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string? RoleCode { get; set; }

    public string? RoleDesc { get; set; }

    public bool? IsActive { get; set; }

    public string? UserGroup { get; set; }

    public virtual ICollection<AuditorUserM> AuditorUserMs { get; set; } = new List<AuditorUserM>();

    public virtual ICollection<HospitalUserM> HospitalUserMs { get; set; } = new List<HospitalUserM>();

    public virtual ICollection<RoleGroupListT> RoleGroupListTs { get; set; } = new List<RoleGroupListT>();

    public virtual ICollection<RoleMenuMapping> RoleMenuMappings { get; set; } = new List<RoleMenuMapping>();

    public virtual ICollection<RoleUserMapping> RoleUserMappings { get; set; } = new List<RoleUserMapping>();

    public virtual ICollection<SsoUserM> SsoUserMs { get; set; } = new List<SsoUserM>();
}
