using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class DataUsesHistoryLog
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? DocName { get; set; }

    public string? DocType { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? EditDate { get; set; }
}
