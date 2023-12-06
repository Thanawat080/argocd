using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalListM
{
    public decimal DentListId { get; set; }

    public string DentDetail { get; set; } = null!;

    public string DentFlag { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string DentGroup { get; set; } = null!;

    public virtual ICollection<AaiDentalCheckD> AaiDentalCheckDs { get; set; } = new List<AaiDentalCheckD>();

    public virtual ICollection<AaiDentalTreatD> AaiDentalTreatDs { get; set; } = new List<AaiDentalTreatD>();
}
