using sso.mms.fees.api.Entities.Dental;

namespace sso.mms.fees.api.ViewModels.Dental
{
    public class AaiDentalCheckDView
    {
        public decimal CheckDId { get; set; }

        public decimal CheckHId { get; set; }

        public decimal DentListId { get; set; }

        public decimal ToothTypeId { get; set; }

        public string Icd10Id { get; set; } = null!;

        public string DoctorName { get; set; } = null!;

        public string Decision { get; set; } = null!;

        public string Icd9Id { get; set; } = null!;

        public DateTime CheckDate { get; set; }

        public decimal? Expense { get; set; }

        public decimal? SsoPay { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        

    }
}
