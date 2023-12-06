using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class AnnounceT
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageFile { get; set; }

    public bool? ActiveStatus { get; set; }
}
