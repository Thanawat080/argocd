using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class ViewMontbudyear
{
    public string BudgetYear { get; set; } = null!;

    public byte? MonthBudyear { get; set; }
}
