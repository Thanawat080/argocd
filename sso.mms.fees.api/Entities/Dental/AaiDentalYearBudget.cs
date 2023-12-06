using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalYearBudget
{
    public decimal BudgetId { get; set; }

    public decimal? BudgetYear { get; set; }

    public decimal BudgetAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
