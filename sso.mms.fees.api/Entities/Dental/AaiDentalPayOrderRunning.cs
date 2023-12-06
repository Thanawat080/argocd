using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalPayOrderRunning
{
    public int PayRunId { get; set; }

    public string YearMonth { get; set; } = null!;

    public int PayRunNumber { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
