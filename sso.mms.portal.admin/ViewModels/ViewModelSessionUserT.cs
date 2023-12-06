namespace sso.mms.portal.admin.ViewModels
{
    public class ViewModelSessionUserT
    {
        public int Id { get; set; }

        public string AccessToken { get; set; } = null!;

        public string ShortToken { get; set; } = null!;

        public bool IsActive { get; set; }

        public int IsStatus { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime UpdateDate { get; set; }

        public string UpdateBy { get; set; } = null!;

        public string? RealmGroup { get; set; }

        public string? ChannelLogin { get; set; }

        public int? HospitalUserMId { get; set; }

        public int? SsoUserMId { get; set; }

        public int? AuditorUserMId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccessType { get; set; }
    }
}
