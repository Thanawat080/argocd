using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthSetRefDoctorM
{
    public decimal SetRefDoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string DeleteStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public decimal ChecklistId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public virtual AaiHealthChecklistM? Checklist { get; set; } = null!;
    public virtual ICollection<AaiHealthCheckupListT> AaiHealthCheckupListTs { get; set; } = new List<AaiHealthCheckupListT>();

}
