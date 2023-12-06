using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthWithdrawalT
{
    public decimal WithdrawalId { get; set; }

    public string WithdrawalNo { get; set; } = null!;

    public string HospitalCode { get; set; } = null!;

    public decimal CheckupListId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public decimal? PayOrderId { get; set; }

    public virtual AaiHealthCheckupListT CheckupList { get; set; } = null!;
}
