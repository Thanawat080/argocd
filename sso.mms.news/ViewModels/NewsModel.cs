using sso.mms.helper.PortalModel;
using System.Collections;

namespace sso.mms.news.ViewModels
{
    public class NewsModel
    {
        public int? Id { get; set; }

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
    }

    public class SearchModel
    {
        public int newsMid { get; set; }
        public string? searchTerm { get; set; }
        // Add other properties as needed
    }
}
