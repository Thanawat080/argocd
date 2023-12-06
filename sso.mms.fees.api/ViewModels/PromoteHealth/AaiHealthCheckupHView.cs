using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public partial class AaiHealthCheckupHView
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

        public bool? IsUd { get; set; }

        public string DeleteStatus { get; set; } = null!;

        public string UseStatus { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public bool IsFromReader { get; set; }

        public string? Reason { get; set; }

        public string? ConfirmBy { get; set; }

        public DateTime? ReadDate { get; set; }

        public string? ReadBy { get; set; }

        public string? BudgetYear { get; set; }

        public string? MonthBudyear { get; set; }

        public string? PatientSurname { get; set; }

        public string PatientTel { get; set; } = null!;

        public string? Remark { get; set; }
        public List<AaiHealthCheckupListTView> AaiHealthCheckupListTs { get; set; } = new List<AaiHealthCheckupListTView>();
    }
    public partial class AaiHealthCheckupListTView
    {
        public decimal CheckupListId { get; set; }

        public decimal CheckupId { get; set; }

        public decimal ChecklistId { get; set; }

        public decimal? SetRefDoctorId { get; set; }

        public string? ResultStatus { get; set; }

        public bool IsChecked { get; set; }

       
        public List<AaiHealthCheckupResultTView> AaiHealthCheckupResultTs { get; set; } =  new List<AaiHealthCheckupResultTView>();

        public AaiHealthChecklistMView Checklist { get; set; } = null!;
        
        public  AaiHealthSetRefDoctorMView SetRefDoctor { get; set; } = null!;
    }
    public class AaiHealthChecklistMView
    {
        public decimal ChecklistId { get; set; }

        public string ChecklistName { get; set; } = null!;

        public decimal ChecklistPrice { get; set; }

        public string ChecklistStatus { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string BudgetYear { get; set; } = null!;

        public bool IsSetRef { get; set; }

        public string? ChecklistCode { get; set; }

    }
    public class AaiHealthCheckupResultTView
    {
        public decimal CheckupResultId { get; set; }

        public decimal CheckupListId { get; set; }

        public decimal ChecklistDtId { get; set; }

        public string? ResultCheckValue { get; set; }

        public string RefValue { get; set; } = null!;

        public bool IsNormal { get; set; }

        public string Suggession { get; set; } = null!;

        public AaiHealthChecklistDView ChecklistDt  { get; set; } = null!;

    }
    public class AaiHealthChecklistDView 
    {
        public decimal ChecklistDtId { get; set; }

        public string ChecklistDtName { get; set; } = null!;

        public decimal ChecklistId { get; set; }

        public bool? IsOption { get; set; }

        public string ChecklistDtStatus { get; set; } = null!;
    }
}
