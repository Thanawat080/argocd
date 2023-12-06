

using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    
    public class HospitalPersonView
    {
        public decimal WithdrawalId { get; set; }

        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public decimal CheckupListId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? WithdrawalDoc { get; set; }

        public List<HosHospitalView> HospitalPerson { get; set; } = new List<HosHospitalView>();

    }
    public partial class HosHospitalView
    {
        public int HospId { get; set; }

        public string HospCode5 { get; set; } = null!;

        public string? HospCode7 { get; set; }

        public string HospCode9 { get; set; } = null!;

        public string HospName { get; set; } = null!;

        public string HospOfficialName { get; set; } = null!;

        public string HospDisplayName { get; set; } = null!;

        public string? HospAddrNo { get; set; }

        public string? HospRoad { get; set; }

        public int? HospSubdistId { get; set; }

        public int? HospDistId { get; set; }

        public int? HospProvId { get; set; }

        public string? HospPostcode { get; set; }

        public string? HospTel { get; set; }

        public string? HospFax { get; set; }

        public string? HospWebsite { get; set; }

        public string? HospEmail { get; set; }

        public string HospType { get; set; } = null!;

        public int? HospTypeId { get; set; }

        public int? AffId { get; set; }

        public int? CorpTypeId { get; set; }

        public string? CorpName { get; set; }

        public string? CorpTax { get; set; }

        public string? CorpAddrNo { get; set; }

        public string? CorpRoad { get; set; }

        public int? CorpSubdistId { get; set; }

        public int? CorpDistId { get; set; }

        public int? CorpProvId { get; set; }

        public string? CorpPostcode { get; set; }

        public string? CorpTel { get; set; }

        public string? CorpFax { get; set; }

        public string? CorpWebsite { get; set; }

        public string? CorpEmail { get; set; }

        public string? HospPerson { get; set; }

        public string CreateUser { get; set; } = null!;

        public DateTime CreateDtm { get; set; }

        public string UpdateUser { get; set; } = null!;

        public DateTime UpdateDtm { get; set; }
        public List<AaiHealthCheckupHViewModel> Person { get; set; }
    }
    public partial class AaiHealthWithdrawalTView
    {
        public decimal WithdrawalId { get; set; }

        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public decimal CheckupListId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? WithdrawalDoc { get; set; }

        public List<HosHospitalView> HospitalPerson { get; set; }
    }
    public partial class AaiHealthCheckupHViewModel
    {
        public decimal CheckupId { get; set; }

        public string? PersonalId { get; set; }

        public string? PatientName { get; set; }

        public string? Hospitalcode { get; set; }

        public string? HospitalName { get; set; }
        public string? PatientSurname { get; set; }

        public int? Allprivilege { get; set; }

        public int? Useprivilege { get; set; }

        public decimal? Sumprice { get; set; }

        public bool ischeck { get; set; }

        public bool isShowAi { get; set; } = false;

        public decimal? withdrawHId { get; set; }

        public decimal? statusAi { get; set; }

        public string? desAi { get; set; }

        public bool isShowRequest { get; set; } = false;

        public string? withdrawNo { get; set; }

    }
    public partial class AaiHealthCheckupListTViewModel
    {
        public decimal CheckupListId { get; set; }

        public decimal CheckupId { get; set; }

        public decimal ChecklistId { get; set; }

        public decimal? SetRefDoctorId { get; set; }

        public string? ResultStatus { get; set; }

        public bool IsChecked { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }
    }
}
