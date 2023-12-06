namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class ManageRecordChecklistView
    {
        public RecordCheckupHView RecordCheckupHView { get; set; } = new RecordCheckupHView();
        public List<diseaseView>? DiseaseViews { get; set; }
        public string? DiseaseReason { get; set; }
        public List<RecordChecklistMView> ChecklistMViews { get; set; } = new List<RecordChecklistMView>();

    }

    public class RecordCheckupHView
    {
        public decimal CheckupId { get; set; }

        public string CheckupNo { get; set; } = null!;

        public decimal? ReserveId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public string? PersonalId { get; set; }

        public string? PatientName { get; set; }

        public string? PatientSex { get; set; }

        public decimal? PatientWeight { get; set; }

        public decimal? PatientHeight { get; set; }

        public string? PatientPressure { get; set; }

        public DateTime? CheckupDate { get; set; }

        public bool? IsUd { get; set; } = false;

        public string DeleteStatus { get; set; } = null!;

        public string UseStatus { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? PatientSurname { get; set; }

        public string PatientTel { get; set; } = null!;

        public decimal? PatientAge { get; set; }

    }
    public class RecordChecklistMView
    {
        public decimal ChecklistId { get; set; }
        public string ChecklistName { get; set; } = null!;
        public decimal ChecklistPrice { get; set; }
        public bool IsSetRef { get; set; }
        public string? ChecklistCode { get; set; }

        public bool StatusCheck { get; set; }

        public bool? IsCheck { get; set; } = false!;

        public string? ResultStatus { get; set; } = "1";

        public decimal? SelectDoctorId { get; set; }

        public decimal? CheckupListId {  get; set; }

        public bool isExport { get; set; } = false;
        public List<RecordChecklistDView>? ChecklistDViews { get; set;}

        public List<RecordSetRefDoctorView>? SetRefDoctorViews { get;set; }

    }

    public class RecordChecklistDView
    {
        public decimal ChecklistDtId { get; set; }

        public string ChecklistDtName { get; set; } = null!;

        public decimal ChecklistId { get; set; }

        public bool? IsOption { get; set; }

        public string ChecklistDtStatus { get; set; } = null!;

        public string? CheckValue {  get; set; }

        public bool? IsNormal { get; set; }

        public string? Suggestion { get; set; }

        public RecordSetRefNickNameView? RecordSetRefNickNameView { get; set; }

        public RecordSetRefChecklistCfgView? RecordSetRefChecklistCfgView { get; set; }

    }

    public class RecordSetRefNickNameView
    {
        public decimal SetRefNnId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public decimal ChecklistDId { get; set; }

        public string SetRefName { get; set; } = null!;
    }

    public class RecordSetRefChecklistCfgView
    {
        public decimal SetRefId { get; set; }

        public decimal ChecklistId { get; set; }

        public decimal ChecklistDtId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public long? StartAge { get; set; }

        public long? EndAge { get; set; }

        public string Sex { get; set; } = null!;

        public string SetRefValue { get; set; }

        public string DeleteStatus { get; set; } = null!;
    }

    public class RecordSetRefDoctorView
    {
        public decimal SetRefDoctorId { get; set; }

        public string DoctorName { get; set; } = null!;

        public string DeleteStatus { get; set; } = null!;

        public decimal ChecklistId { get; set; }

        public string HospitalCode { get; set; } = null!;
    }
    public class diseaseView
    {
        public int Id { get; set; }
        public string value { get; set; }
        public bool active { get; set; }

       

    }

    public class saverecord
    {
        public string usernameupdate { get; set; }
        public string? hoscode { get; set; }

        public bool isDisease { get; set; }

        public string remarkdisease { get; set; }
        public List<CheckPermissionCheckListView> person { get; set; }
        public List<diseaseView> diseaseView { get; set; }

    }

   
}
