using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class AaiHealthSetRefChecklistCfgView
    {
        public decimal SetRefId { get; set; } = default(decimal);

        public decimal? ChecklistId { get; set; }

        public decimal? ChecklistDtId { get; set; }

        public string? HospitalCode { get; set; }

        public long? StartAge { get; set; }

        public long? EndAge { get; set; }

        public string Sex { get; set; } = null!;

        public string SetRefValue { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }
}
