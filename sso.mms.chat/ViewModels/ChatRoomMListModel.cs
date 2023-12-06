using sso.mms.helper.PortalModel;

namespace sso.mms.chat.ViewModels
{
    public class ChatRoomMListModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public List<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
