using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class ViewAaiHealthCheckupListT
{
    public string HospitalCode { get; set; } = null!;

    public DateTime? CheckupDateH { get; set; }

    public decimal? ReserveIdH { get; set; }

    public bool IsFromReader { get; set; }

    public decimal? CheckupListId { get; set; }

    public decimal? ReserveId { get; set; }

    public string? BudgetYear { get; set; }

    public string? PersonalId { get; set; }

    public string? PatientName { get; set; }

    public string? PatientSurname { get; set; }

    public string? MonthBudyear { get; set; }

    public string? Reason { get; set; }

    public DateTime? CheckupDate { get; set; }

    public decimal CheckupId { get; set; }

    public string DeleteStatus { get; set; } = null!;

    public string? PatientSex { get; set; }

    public string PatientTel { get; set; } = null!;

    public string UseStatus { get; set; } = null!;

    public decimal? ChecklistId { get; set; }

    public string? ChecklistName { get; set; }

    public decimal? ChecklistPrice { get; set; }

    public string? ChecklistCode { get; set; }

    public bool? IsChecked { get; set; }

    public DateTime? ReadDate { get; set; }

    public bool? IsExport { get; set; }
}
