using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class SettingM
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public bool? Isactive { get; set; }

    public int? Isstatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Type { get; set; }
}
