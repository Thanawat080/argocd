using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthWithdrawalH
{
    public decimal WithdrawalHId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string WithdrawalNo { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? WithdrawalDoc { get; set; }

    public string? Proactive { get; set; }

    public decimal? SuggestionStatus { get; set; }

    public string? SuggestionDesc { get; set; }

    public decimal? JobId { get; set; }

    public decimal CheckupId { get; set; }

    public DateTime? TreatmentDate { get; set; }

    public bool? PidManual { get; set; }
}
