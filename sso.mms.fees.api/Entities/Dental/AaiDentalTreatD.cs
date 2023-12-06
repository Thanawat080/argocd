using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalTreatD
{
    public decimal TreatId { get; set; }

    public decimal CheckHId { get; set; }

    public decimal DentListId { get; set; }

    public string? ToothNumber { get; set; }

    public decimal ToothItem { get; set; }

    public decimal? Expense { get; set; }

    public decimal? SsoPay { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual AaiDentalCheckH CheckH { get; set; } = null!;

    public virtual AaiDentalListM DentList { get; set; } = null!;
}
