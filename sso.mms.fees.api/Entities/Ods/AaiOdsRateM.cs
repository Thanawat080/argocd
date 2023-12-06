using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Ods;

public partial class AaiOdsRateM
{
    public byte RateodsId { get; set; }

    public byte BudgetYear { get; set; }

    public byte Rateods { get; set; }

    public string RateodsActive { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public virtual AaiOdsBudgetYearM BudgetYearNavigation { get; set; } = null!;
}
