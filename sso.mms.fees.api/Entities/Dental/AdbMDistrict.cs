using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AdbMDistrict
{
    public int DistId { get; set; }

    public string? DistCode { get; set; }

    public string? DistName { get; set; }

    public string? DistStatus { get; set; }

    public int? ProvId { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtm { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDtm { get; set; }
}
