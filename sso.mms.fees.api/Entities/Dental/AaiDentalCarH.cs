using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalCarH
{
    public decimal DentalCarHId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string PlaceName { get; set; } = null!;

    public DateTime ServiceDate { get; set; }

    public DateTime ServiceStartDate { get; set; }

    public DateTime ServiceEndDate { get; set; }

    public string PlaceProvince { get; set; } = null!;

    public string PlaceDistrict { get; set; } = null!;

    public string PlaceSubDistrict { get; set; } = null!;

    public string? RegisterDoc { get; set; }

    public string? DantalCarStatus { get; set; }

    public string? ChangeDesc { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public int? SsoOrgId { get; set; }

    public string RegisterDocFileName { get; set; } = null!;

    public virtual ICollection<AaiDentalCarD> AaiDentalCarDs { get; set; } = new List<AaiDentalCarD>();
}
