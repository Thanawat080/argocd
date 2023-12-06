namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class AaiHealthSetRefDoctorView
    {
        public decimal SetRefDoctorId { get; set; }

        public string? DoctorName { get; set; } = null!;

        public string? DeleteStatus { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public decimal ChecklistId { get; set; }

        public string HospitalCode { get; set; }
    }
}
