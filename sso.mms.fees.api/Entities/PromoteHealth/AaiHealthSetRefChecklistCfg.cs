using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthSetRefChecklistCfg
{
    public decimal SetRefId { get; set; }

    public decimal ChecklistId { get; set; }

    public decimal ChecklistDtId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public long? StartAge { get; set; }

    public long? EndAge { get; set; }

    public string Sex { get; set; } = null!;

    public string SetRefValue { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string DeleteStatus { get; set; } = null!;

    public virtual AaiHealthChecklistM? Checklist { get; set; } = null!;

    public virtual AaiHealthChecklistD? ChecklistDt { get; set; } = null!;
}
