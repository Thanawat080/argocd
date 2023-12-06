using System.ComponentModel.DataAnnotations;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class AaiHealthSetRefDoctorMView
    {
        public decimal SetRefDoctorId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกชื่อผู้วินิจฉัย")]
        public string DoctorName { get; set; } = null!;

        public string DeleteStatus { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public decimal ChecklistId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public bool Edit { get; set; } = false;

        public bool Delete { get; set; } = false;

        
    }
    public class DataSaveDoctor 
    {
        public string? username { get; set; } = "";
        public List<AaiHealthSetRefDoctorMView> doctor { get; set; }
    }

}
