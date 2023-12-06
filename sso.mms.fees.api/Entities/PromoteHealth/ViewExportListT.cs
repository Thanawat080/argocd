using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class ViewExportListT
{
    public decimal? CheckupListId { get; set; }

    public string MainHos { get; set; } = null!;

    public DateTime? TreatmentDate { get; set; }

    public decimal? Proactive { get; set; }

    public decimal? PidManual { get; set; }

    public string? PersonalId { get; set; }

    public decimal CheckupId { get; set; }
}
