using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class SsoOtpT
{
    public int Id { get; set; }

    public string? OtpCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? RealmGroup { get; set; }

    public string? OtpType { get; set; }
}
