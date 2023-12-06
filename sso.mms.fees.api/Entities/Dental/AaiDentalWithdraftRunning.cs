using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalWithdraftRunning
{
    public decimal DocuId { get; set; }

    public string DocuType { get; set; } = null!;

    public string SsoOrgCode { get; set; } = null!;

    public string YearMonth { get; set; } = null!;

    public decimal DocuLastNo { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
