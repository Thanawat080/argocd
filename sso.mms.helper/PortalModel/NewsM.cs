using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class NewsM
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Remark { get; set; }

    public string? ImagePathM { get; set; }

    public string? ImageFileM { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public int? NewsType { get; set; }

    public virtual ICollection<NewsT> NewsTs { get; set; } = new List<NewsT>();
}
