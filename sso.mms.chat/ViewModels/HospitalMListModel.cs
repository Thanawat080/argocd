
namespace sso.mms.chat.ViewModels
{
    public class HospitalMListModel
    {
        public int RoomId { get; set; }
        public int? HospitalMId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? ImagePath { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }
}
