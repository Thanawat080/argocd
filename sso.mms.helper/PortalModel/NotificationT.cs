using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class NotificationT
{
    public int Id { get; set; }

    public int? NotiMId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? UserType { get; set; }

    public string? NotifyOption { get; set; }

    public string? OrgCode { get; set; }

    public string? RoleCode { get; set; }

    public string? UserName { get; set; }

    public string? AppCode { get; set; }

    public string? Url { get; set; }

    public string? UrlText { get; set; }

    public string? IdRef { get; set; }

    public int? NewTId { get; set; }

    public virtual NewsT? NewT { get; set; }

    public virtual NotificationM? NotiM { get; set; }

    public virtual ICollection<NotificationLog> NotificationLogs { get; set; } = new List<NotificationLog>();
}
