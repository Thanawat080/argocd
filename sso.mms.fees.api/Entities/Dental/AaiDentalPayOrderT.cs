using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalPayOrderT
{
    public int PayOrderId { get; set; }

    public string? WithdrawalNo { get; set; }

    public int? HospitalId { get; set; }

    public int? SsoOrgCode { get; set; }

    public string DentGroup { get; set; } = null!;

    public string PersonalId { get; set; } = null!;

    public string? PayOrderSetNo { get; set; }

    public string? PayOrderNo { get; set; }

    public decimal? PayOrderAmt { get; set; }

    public string? PayOrderStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? SignDate { get; set; }

    public string? SignBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public string? ApproveBy { get; set; }

    public virtual AaiDentalWithdrawT? WithdrawalNoNavigation { get; set; }
}
