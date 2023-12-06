using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Ods;

public partial class AaiOdsWithdrawalT
{
    public decimal WithdrawalId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string? AdbId { get; set; }

    public string? TranId { get; set; }

    public byte RateodsNow { get; set; }

    public DateTime? PeriodDate { get; set; }

    public string WithdrawalNo { get; set; } = null!;

    public string? Status { get; set; }

    public string? WithdrawalDoc { get; set; }

    public string? IsPreaudit { get; set; }

    public DateTime? CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<AaiOdsPayOrderT> AaiOdsPayOrderTs { get; set; } = new List<AaiOdsPayOrderT>();
}
