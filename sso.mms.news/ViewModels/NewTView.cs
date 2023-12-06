using sso.mms.helper.PortalModel;

namespace sso.mms.news.ViewModels
{
    public class NewsTView
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? ImagePathM { get; set; }

        public string? ImageFileM { get; set; }

        public string? UploadPath { get; set; }

        public string? UploadFile { get; set; }

        public int? NewsMId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public int? PinStatus { get; set; }

        public bool? PrivilegePublic { get; set; }

        public bool? PrivilegePrivate { get; set; }

        public bool? PrivilegeSso { get; set; }

        public virtual NewsM? NewsM { get; set; }

        public virtual ICollection<NewsTagList> NewsTagLists { get; set; } = new List<NewsTagList>();

        public virtual ICollection<NotificationT> NotificationTs { get; set; } = new List<NotificationT>();
        public string? DepName { get; set; } 
        public string? PosName { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
    }
    public partial class NewsMView
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Remark { get; set; }

        public string? ImagePathM { get; set; }

        public string? ImageFileM { get; set; }

        public bool? IsActive { get; set; }
        
        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public int? NewsType { get; set; }

        public virtual ICollection<NewsTView> NewsTs { get; set; } = new List<NewsTView>();
    }
}
