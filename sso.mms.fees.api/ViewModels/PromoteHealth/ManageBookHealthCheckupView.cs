using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.ComponentModel.DataAnnotations;
namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class ManageBookHealthCheckupView
    {
        public List<CheckPermissionCheckListView> person { get; set; }
        public ReserveH dataCompany { get; set; }
    }

    public class ReserveH
    {
        public decimal ReserveId { get; set; }
        public string HospitalCode { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกชื่อสถานประกอบการ")]
        public string CompanyName { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกเลขสาขา")]
        public string CompanyBranch { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกเลขที่นิติบุคคล")]
        public string CompanyTaxNo { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกเลขที่บัญชีนายจ้าง")]
        public string CompanyAccNo { get; set; } = null!;

        [Required(ErrorMessage = "กรุณากรอกที่อยู่สถานประกอบการ")]
        public string CompanyAddr { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกชื่อ HR")]
        public string HrName { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกอีเมล HR")]
        public string HrEmail { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกเบอร์โทรศัพท์ HR")]
        public string HrPhone { get; set; } = null!;
        [Required(ErrorMessage = "กรุณากรอกวันที่เริ่มตรวจ")]
        public DateTime? ReserveStartDate { get; set; }
        [Required(ErrorMessage = "กรุณากรอกวันที่สิ้นสุดการตรวจ")]
        public DateTime? ReserveEndDate { get; set; }

        public string? DocStatus { get; set; } = null!;

        public string CreateBy { get; set; } = null!;

        public string? UpdateBy { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
