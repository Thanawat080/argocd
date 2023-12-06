using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthSetUnderlyingDiseaseM
{
    public decimal UdId { get; set; }

    public decimal CheckupId { get; set; }

    public bool? IsDb { get; set; }

    public bool? IsHpldm { get; set; }

    public bool? IsObst { get; set; }

    public bool? IsHpts { get; set; }

    public bool? IsCvds { get; set; }

    public bool? IsCopd { get; set; }

    public bool? IsCkd { get; set; }

    public bool? IsCc { get; set; }

    public bool? IsEps { get; set; }

    public bool? IsDps { get; set; }

    public bool? IsTlsm { get; set; }

    public bool? IsOther { get; set; }

    public string? Remark { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual AaiHealthCheckupH Checkup { get; set; } = null!;
}
