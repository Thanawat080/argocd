using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthCheckupResultT
{
    public decimal CheckupResultId { get; set; }

    public decimal CheckupListId { get; set; }

    public decimal ChecklistDtId { get; set; }

    public string? ResultCheckValue { get; set; }

    public string? RefValue { get; set; }

    public bool? IsNormal { get; set; }

    public string? Suggession { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual AaiHealthChecklistD ChecklistDt { get; set; } = null!;

    public virtual AaiHealthCheckupListT CheckupList { get; set; } = null!;
}
