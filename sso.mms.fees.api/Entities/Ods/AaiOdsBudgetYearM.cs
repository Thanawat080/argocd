using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Ods;

public partial class AaiOdsBudgetYearM
{
    public byte BudgetYear { get; set; }

    public bool BudgetYearStatus { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public virtual ICollection<AaiOdsRateM> AaiOdsRateMs { get; set; } = new List<AaiOdsRateM>();
}
