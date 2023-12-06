using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class SubdistrictM
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? DistrictCode { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual DistrictM? DistrictCodeNavigation { get; set; }

    public virtual ICollection<HospitalUserM> HospitalUserMs { get; set; } = new List<HospitalUserM>();
}
