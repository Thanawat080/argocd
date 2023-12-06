using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class DistrictM
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public string? ProvinceCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<HospitalUserM> HospitalUserMs { get; set; } = new List<HospitalUserM>();

    public virtual ProvinceM? ProvinceCodeNavigation { get; set; }

    public virtual ICollection<SubdistrictM> SubdistrictMs { get; set; } = new List<SubdistrictM>();
}
