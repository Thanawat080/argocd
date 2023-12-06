using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalWithdrawD
{
    public decimal WithdrawId { get; set; }

    public string? WithdrawNo { get; set; }

    public decimal? CheckDId { get; set; }

    public decimal? CheckHId { get; set; }

    public decimal? Expense { get; set; }

    public decimal? SsoPay { get; set; }

    public string? CheckStatus { get; set; }

    public DateTime? WithdrawDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual AaiDentalCheckD? CheckD { get; set; }

    public virtual AaiDentalCheckH? CheckH { get; set; }
}
