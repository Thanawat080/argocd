using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class NotificationM
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? NotiCode { get; set; }

    public string? AppCode { get; set; }

    public int? Sequence { get; set; }

    public virtual ICollection<NotificationT> NotificationTs { get; set; } = new List<NotificationT>();
}
