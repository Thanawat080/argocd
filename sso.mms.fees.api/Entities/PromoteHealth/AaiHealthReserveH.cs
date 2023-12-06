using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthReserveH
{
    public decimal ReserveId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyBranch { get; set; } = null!;

    public string CompanyTaxNo { get; set; } = null!;

    public string CompanyAccNo { get; set; } = null!;

    public string CompanyAddr { get; set; } = null!;

    public string HrName { get; set; } = null!;

    public string HrEmail { get; set; } = null!;

    public string HrPhone { get; set; } = null!;

    public DateTime ReserveStartDate { get; set; }

    public DateTime ReserveEndDate { get; set; }

    public string DeleteStatus { get; set; } = null!;

    public string DocStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CancleDate { get; set; }

    public string? CancleBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public string? DeleteBy { get; set; }

    public virtual ICollection<AaiHealthCheckupH> AaiHealthCheckupHs { get; set; } = new List<AaiHealthCheckupH>();
}
