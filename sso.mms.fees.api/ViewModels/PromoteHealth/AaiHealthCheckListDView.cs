using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class AaiHealthCheckListDView
    {
        public decimal ChecklistDtId { get; set; }

        public string ChecklistDtName { get; set; } = null!;

        public decimal ChecklistId { get; set; }

        public bool? IsOption { get; set; }

        public bool ChecklistDtStatus { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public bool Edit { get; set; } = false;


    }
}
