using sso.mms.helper.PortalModel;

namespace sso.mms.news.ViewModels
{
    public class NewsTagListModel
    {
        public int Id { get; set; }

        public int? NewsTId { get; set; }

        public string[]? TagName { get; set; }   

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

    }


    
}
