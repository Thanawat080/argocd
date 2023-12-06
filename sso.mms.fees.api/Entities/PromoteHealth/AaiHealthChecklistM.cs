using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthChecklistM
{
    public decimal ChecklistId { get; set; }

    public string ChecklistName { get; set; } = null!;

    public decimal ChecklistPrice { get; set; }

    public string ChecklistStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string BudgetYear { get; set; } = null!;

    public bool IsSetRef { get; set; }

    public string? ChecklistCode { get; set; }

    public string? ChecklistShortname { get; set; }

    public virtual ICollection<AaiHealthChecklistD> AaiHealthChecklistDs { get; set; } = new List<AaiHealthChecklistD>();

    public virtual ICollection<AaiHealthCheckupListT> AaiHealthCheckupListTs { get; set; } = new List<AaiHealthCheckupListT>();

    public virtual ICollection<AaiHealthSetRefChecklistCfg> AaiHealthSetRefChecklistCfgs { get; set; } = new List<AaiHealthSetRefChecklistCfg>();

    public virtual ICollection<AaiHealthSetRefDoctorM> AaiHealthSetRefDoctorMs { get; set; } = new List<AaiHealthSetRefDoctorM>();
}
