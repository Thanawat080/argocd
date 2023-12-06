using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class AutMMenu
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
