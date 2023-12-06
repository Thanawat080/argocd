using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalWithdrawT
{
    public int WithdrawId { get; set; }

    public string WithdrawNo { get; set; } = null!;

    public decimal CheckDId { get; set; }

    public decimal CheckHId { get; set; }

    public decimal Expense { get; set; }

    public decimal SsoPay { get; set; }

    public string CheckStatus { get; set; } = null!;

    public DateTime WithdrawDate { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public virtual ICollection<AaiDentalPayOrderT> AaiDentalPayOrderTs { get; set; } = new List<AaiDentalPayOrderT>();

    public virtual AaiDentalCheckD CheckD { get; set; } = null!;

    public virtual AaiDentalCheckH CheckH { get; set; } = null!;
}
