using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleSystemM
{
    public long Id { get; set; }

    public string? SystemCode { get; set; }

    public string? Name { get; set; }

    public string? SystemDesc { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
