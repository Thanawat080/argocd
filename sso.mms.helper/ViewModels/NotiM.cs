namespace sso.mms.helper.ViewModels
{
    public class NotiM
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

        public string DivClass { get; set; }

        public int? Sequence { get; set; }

        public string? NotiCode { get; set; }

        public virtual ICollection<NotiT> NotificationTs { get; set; } = new List<NotiT>();
    }
}
