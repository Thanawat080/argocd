namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class GetPaymentOrderList
    {
        public decimal? WithdrawalId { get; set; }

        public string? WithdrawalNo { get; set; } = null!;

        public string? HospitalCode { get; set; } = null!;

        public decimal? CheckupListId { get; set; }

        public string? Status { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public decimal? ChecklistId { get; set; }

        public string? ChecklistName { get; set; }

        public decimal? ChecklistPrice { get; set; }

        public string? ChecklistCode { get; set; }
        public decimal? SumPrice { get; set; }
        public int? HospitalCount { get; set; }
        public int? PersonCount { get; set; }
        public int? PersonCountAll { get; set; }
        public string? Displayperson { get; set; }
        public string? Displayprice { get; set; }
        public decimal? SumPriceAll { get; set; }
        public string? HospCode { get; set; }
        public string? HospName { get; set; }
        public string? HospDisplayName { get; set; }
        public bool isShowAi { get; set; } = false;
        public bool ischeck { get; set; }

        public string? WithdrawalDoc { get; set; }
        public bool isRequestForm { get; set; } = false;
    }
    public class SaveOrderT 
    { 
     public List<GetPaymentOrderList>? HospByWitdraw { get; set; }
     public GetPaymentOrderList? HospitalList { get; set; }

    public List<AaiHealthCheckupHViewModel> person { get; set; }
    public string? InjectwdNo { get; set; }

    public string? usernameupdate { get; set; }

    }

    public class GetPayOrderListT
    {
        public string? PayOrderSetNo { get; set; } = null!;

        public string? PayOrderNo { get; set; } = null!;

        public string? WithdrawalNo { get; set; } = null!;

        public string? HospitalCode { get; set; } = null!;

        public string? PayOrderStatus { get; set; } = null!;

        public DateTime? ApproveDate { get; set; }
        public int? CountHos { get; set; }

        public decimal Allprice { get; set; }

        public string? updateBy { get; set; }

        public bool ischeck { get; set; } = false;

    }

    public class AaiHealthWithdrawalHViewForAi
    {
        public string HospitalCode { get; set; } = null!;
        public string HospitalName { get; set; } = null!;
        public string WithdrawalNo { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public decimal? SuggestionStatus { get; set; }
        public string? SuggestionDesc { get; set; }
        public string? Name { get; set; }

    }

    public class HospitalAiList
    {
        public string Hoscode { get; set; }
        public string Hosname { get; set; }

        public int AllChecklist { get; set; }

        public decimal Allprice { get; set; }


    }

    public class HospitalAi
    {
        public int AllHospital { get; set; }
        public int Allperson { get; set; }

        public List<HospitalAiList> hospitalAiList { get; set; }

    }


    public class PersonAiList
    {
        public string? Fullname { get; set; }
        public string? Personalid { get; set; }

        public DateTime? CheckIndate { get; set; }

        public string ProActive { get; set; }
        public decimal OutlierType { get; set; }
        public string Message { get; set; }


    }
    public class PersonForAi
    {
        public int AllpersonByHos { get; set; }

        public List<PersonAiList> personAiList { get; set; }

    }

}
