using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class ChatRoomM
{
    public int Id { get; set; }

    public string? CodeHos { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? CodeSso { get; set; }

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
}
