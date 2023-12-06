using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class ChatRoom
{
    public int Id { get; set; }

    public int? ChatRoomMId { get; set; }

    public int? HospitalMId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ChatRoomM? ChatRoomM { get; set; }

    public virtual ICollection<ChatT> ChatTs { get; set; } = new List<ChatT>();

    public virtual HospitalM? HospitalM { get; set; }
}
