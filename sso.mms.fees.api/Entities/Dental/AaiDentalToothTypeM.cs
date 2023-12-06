using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalToothTypeM
{
    public decimal ToothTypeId { get; set; }

    public string ToothNo { get; set; } = null!;

    public string ToothName { get; set; } = null!;

    public string ToothType { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public virtual ICollection<AaiDentalCheckD> AaiDentalCheckDs { get; set; } = new List<AaiDentalCheckD>();
}
