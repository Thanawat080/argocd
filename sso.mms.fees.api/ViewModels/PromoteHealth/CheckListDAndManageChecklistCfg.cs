namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class CheckListDAndManageChecklistCfg
    {
        public decimal ChecklistDtId { get; set; }

        public string ChecklistDtName { get; set; } = null!;

        public decimal ChecklistId { get; set; }

        public bool? IsOption { get; set; }

        public string ChecklistDtStatus { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public List<CheckListCfgView> CheckListCfgView { get; set; }
    }

    public class CheckListCfgView
    {
        public decimal SetRefId { get; set; }

        public decimal ChecklistId { get; set; }

        public decimal ChecklistDtId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public long? StartAge { get; set; }

        public long? EndAge { get; set; }

        public string Sex { get; set; } = null!;

        public string SetRefValue { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string DeleteStatus { get; set; } = null!;

        public bool Delete { get; set; } = false;
    }
}
