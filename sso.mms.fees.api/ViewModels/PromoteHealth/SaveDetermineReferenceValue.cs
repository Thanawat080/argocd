using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class SaveDetermineReferenceValue
    {
        public List<ChecklistDAndSetRefNickNameView>? savenickname { get; set; }
        //public List<AaiHealthSetRefDoctorMView>? savedoctor { get; set; }
        public List<CheckListDAndManageChecklistCfg>? saveconfig { get; set; }

        public string? username { get; set; }

    }


    public class ChecklistDAndSetRefNickNameView
    {
        public decimal? ChecklistDtId { get; set; }

        public string? ChecklistDtName { get; set; } = null!;
        public bool? IsOption { get; set; }

        public string ChecklistDtStatus { get; set; } = null!;

        public modelnickname nickname { get; set; }

    }


    public class modelnickname
    {


        public decimal? SetRefNnId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public decimal? ChecklistDId { get; set; }

        public string? SetRefName { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
        public bool Edit { get; set; } = false;
    }
}
