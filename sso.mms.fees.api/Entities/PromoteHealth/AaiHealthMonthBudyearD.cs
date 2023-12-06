using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthMonthBudyearD
{
    public byte MonthBudyear { get; set; }

    public byte BudgetYear { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }
}
