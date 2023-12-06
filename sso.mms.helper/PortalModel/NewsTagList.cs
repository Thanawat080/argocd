using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class NewsTagList
{
    public int Id { get; set; }

    public string? TagName { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public int? NewsTId { get; set; }

    public virtual NewsT? NewsT { get; set; }
}
