using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AdbMSubdistrict
{
    public int SubdistId { get; set; }

    public string? SubdistCode { get; set; }

    public string? SubdistName { get; set; }

    public string? SubdistStatus { get; set; }

    public int? DistId { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtm { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDtm { get; set; }
}
