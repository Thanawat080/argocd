using Microsoft.AspNetCore.Components.Forms;
using sso.mms.helper.PortalModel;

namespace sso.mms.portal.ext.ViewModels
{
    public class RequestCertificate
    {
       
        public int Id { get; set; }

        public string? UploadImagePath { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
        public string? UploadImageName { get; set; }

        public string? UploadFileName { get; set; }

        public string? UploadFilePath { get; set; }
        public IBrowserFile File { get; set; }

        public int? HospitalMId { get; set; }
    }


  
}