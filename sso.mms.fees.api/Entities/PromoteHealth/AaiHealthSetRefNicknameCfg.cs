using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class AaiHealthSetRefNicknameCfg
{
    public decimal SetRefNnId { get; set; }

    public string HospitalCode { get; set; } = null!;

    public decimal ChecklistDId { get; set; }

    public string SetRefName { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
