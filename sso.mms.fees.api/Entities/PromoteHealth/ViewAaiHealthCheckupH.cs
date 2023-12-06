using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class ViewAaiHealthCheckupH
{
    public decimal CheckupId { get; set; }

    public string? PersonalId { get; set; }

    public string? PatientName { get; set; }

    public string? PatientSurname { get; set; }

    public string? BudgetYear { get; set; }

    public string? MonthBudyear { get; set; }

    public string HospitalCode { get; set; } = null!;

    public decimal? CountWithdraw { get; set; }

    public decimal? TotalPrice { get; set; }
}
