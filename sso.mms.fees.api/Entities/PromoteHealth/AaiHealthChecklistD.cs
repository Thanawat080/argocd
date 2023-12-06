using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthChecklistD
{
    public decimal ChecklistDtId { get; set; }

    public string ChecklistDtName { get; set; } = null!;

    public decimal ChecklistId { get; set; }

    public bool? IsOption { get; set; }

    public string ChecklistDtStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<AaiHealthCheckupResultT>? AaiHealthCheckupResultTs { get; set; } = new List<AaiHealthCheckupResultT>();

    public virtual ICollection<AaiHealthSetRefChecklistCfg> AaiHealthSetRefChecklistCfgs { get; set; } = new List<AaiHealthSetRefChecklistCfg>();

    public virtual AaiHealthChecklistM? Checklist { get; set; } = null!;
}
