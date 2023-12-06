using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthBudgetYearM
{
    public string BudgetYear { get; set; } = null!;

    public bool? BudgetYearStatus { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }
}
