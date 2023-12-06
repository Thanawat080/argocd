using Microsoft.AspNetCore.Components.Forms;

namespace sso.mms.portal.admin.ViewModels
{
    public class AnnounceModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageFile { get; set; }

        public IBrowserFile File { get; set; }

        public bool? ActiveStatus { get; set; }
    }
}
