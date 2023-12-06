using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthPayOrderT
{
    public decimal PayOrderId { get; set; }

    public string PayOrderSetNo { get; set; } = null!;

    public string PayOrderNo { get; set; } = null!;

    public string WithdrawalNo { get; set; } = null!;

    public string HospitalCode { get; set; } = null!;

    public string PayOrderStatus { get; set; } = null!;

    public bool? PayOrderRisk { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? SignDate { get; set; }

    public string? SignBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public string? ApproveBy { get; set; }

    public decimal? PayAmount { get; set; }
}
