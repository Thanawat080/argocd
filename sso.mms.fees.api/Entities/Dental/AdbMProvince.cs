using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AdbMProvince
{
    public int ProvId { get; set; }

    public string? ProvCode { get; set; }

    public string? ProvName { get; set; }

    public string? ProvStatus { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtm { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDtm { get; set; }
}
