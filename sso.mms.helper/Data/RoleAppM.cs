using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class RoleAppM
{
    public int Id { get; set; }

    public string AppCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string SystemDesc { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int IsStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string Url { get; set; } = null!;
}
