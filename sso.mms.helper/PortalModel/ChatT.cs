using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class ChatT
{
    public int Id { get; set; }

    public int? ChatRoomId { get; set; }

    public string? TextMessage { get; set; }

    public int? UserType { get; set; }

    public string? SenderName { get; set; }

    public int? RefChatId { get; set; }

    public string? UploadFilePath { get; set; }

    public string? UploadFileName { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? UploadImageName { get; set; }

    public string? UploadImagePath { get; set; }

    public int? IsRead { get; set; }

    public virtual ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();

    public virtual ChatRoom? ChatRoom { get; set; }
}
