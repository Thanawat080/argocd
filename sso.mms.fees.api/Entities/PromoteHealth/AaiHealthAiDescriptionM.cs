using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthAiDescriptionM
{
    public decimal Id { get; set; }

    public string Message { get; set; } = null!;

    public decimal? IsStatus { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
