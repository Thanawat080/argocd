using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class AutMApp
{
    public int Id { get; set; }

    public string? AppCode { get; set; }

    public string? Name { get; set; }

    public string? SystemDesc { get; set; }

    public string? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Url { get; set; }
}
