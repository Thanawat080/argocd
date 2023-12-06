using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class ProvinceM
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<DistrictM> DistrictMs { get; set; } = new List<DistrictM>();

    public virtual ICollection<HospitalUserM> HospitalUserMs { get; set; } = new List<HospitalUserM>();
}
