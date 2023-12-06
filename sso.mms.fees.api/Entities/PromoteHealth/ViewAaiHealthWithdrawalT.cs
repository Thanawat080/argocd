using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class ViewAaiHealthWithdrawalT
{
    public decimal WithdrawalId { get; set; }

    public string WithdrawalNo { get; set; } = null!;

    public string HospitalCode { get; set; } = null!;

    public decimal CheckupListId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public decimal? ChecklistId { get; set; }

    public string? ChecklistName { get; set; }

    public decimal? ChecklistPrice { get; set; }

    public string? ChecklistCode { get; set; }
}
