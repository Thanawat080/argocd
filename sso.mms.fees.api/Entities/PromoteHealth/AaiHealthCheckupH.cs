using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthCheckupH
{
    public decimal CheckupId { get; set; }

    public string CheckupNo { get; set; } = null!;

    public decimal? ReserveId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string? PersonalId { get; set; }

    public string? PatientName { get; set; }

    public string? PatientSex { get; set; }

    public decimal? PatientWeight { get; set; }

    public decimal? PatientHeight { get; set; }

    public string? PatientPressure { get; set; }

    public DateTime? CheckupDate { get; set; }

    public bool? IsUd { get; set; }

    public string DeleteStatus { get; set; } = null!;

    public string UseStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public bool IsFromReader { get; set; }

    public string? Reason { get; set; }

    public string? ConfirmBy { get; set; }

    public DateTime? ReadDate { get; set; }

    public string? ReadBy { get; set; }

    public string? BudgetYear { get; set; }

    public string? MonthBudyear { get; set; }

    public string? PatientSurname { get; set; }

    public string PatientTel { get; set; } = null!;

    public string? Remark { get; set; }

    public decimal? PatientAge { get; set; }

    public virtual ICollection<AaiHealthCheckupListT> AaiHealthCheckupListTs { get; set; } = new List<AaiHealthCheckupListT>();

    public virtual ICollection<AaiHealthSetUnderlyingDiseaseM> AaiHealthSetUnderlyingDiseaseMs { get; set; } = new List<AaiHealthSetUnderlyingDiseaseM>();

    public virtual AaiHealthReserveH? Reserve { get; set; }
}
