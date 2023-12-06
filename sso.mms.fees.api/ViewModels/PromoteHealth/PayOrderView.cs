namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class PayOrderView
    {
        public decimal PayOrderId { get; set; }

        public string PayOrderSetNo { get; set; } = null!;

        public string PayOrderNo { get; set; } = null!;

        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public string PayOrderStatus { get; set; } = null!;

        public decimal? PayAmount { get; set; }

        public DateTime? SignDate { get; set; }

        public string? SignBy { get; set; }

        public DateTime? ApproveDate { get; set; }

        public string? ApproveBy { get; set; }
    }

    public class PayOrderDetailView
    {
        public string PersonalId { get; set; }
        public string? PersonalName { get; set; }
        public string? PersonalSurname { get; set; }
        public string? CountChecklistM { get; set; }

        public string? CountChecklistPrice { get; }
    }


    public class PayOrderHistoryView
    {
        public string WithdrawalNo { get; set; }

        public string PayOrderSetNo { get; set; } = null!;

        public int CountHospitals { get; set; } = 0!;

        public DateTime? DateApprove { get; set; }

        public string Status { get; set; } = null!;

        public decimal? SumChecklistPrice { get; set; }
    }


}
