using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalCarD
{
    public decimal DentalCarDId { get; set; }

    public decimal DentalCarHId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string? Remark { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string CarType { get; set; } = null!;

    public virtual ICollection<AaiDentalCheckH> AaiDentalCheckHs { get; set; } = new List<AaiDentalCheckH>();

    public virtual AaiDentalCarH DentalCarH { get; set; } = null!;
}
