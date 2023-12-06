using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthCheckupListT
{
    public decimal CheckupListId { get; set; }

    public decimal CheckupId { get; set; }

    public decimal ChecklistId { get; set; }

    public decimal? SetRefDoctorId { get; set; }

    public string? ResultStatus { get; set; }

    public bool IsChecked { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsExport { get; set; }

    public virtual ICollection<AaiHealthCheckupResultT> AaiHealthCheckupResultTs { get; set; } = new List<AaiHealthCheckupResultT>();

    public virtual ICollection<AaiHealthWithdrawalT> AaiHealthWithdrawalTs { get; set; } = new List<AaiHealthWithdrawalT>();

    public virtual AaiHealthChecklistM Checklist { get; set; } = null!;

    public virtual AaiHealthCheckupH Checkup { get; set; } = null!;

    public virtual AaiHealthSetRefDoctorM? SetRefDoctor { get; set; }
}
