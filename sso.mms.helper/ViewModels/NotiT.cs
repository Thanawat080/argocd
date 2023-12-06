using sso.mms.helper.PortalModel;
namespace sso.mms.helper.ViewModels
{
    public class NotiT
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

        public string? DivClass { get; set; }

        public string? AppCode { get; set; }


        public virtual ICollection<NotificationLog> NotificationLogs { get; set; } = new List<NotificationLog>();
    }
}
