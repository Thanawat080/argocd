namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class GetRecordChecklistView
    {
        public decimal CheckupId { get; set; }

        public string CheckupNo { get; set; } = null!;

        public decimal? ReserveId { get; set; }

        public string HospitalCode { get; set; } = null!;

        public string? PersonalId { get; set; }

        public string? PatientName { get; set; }
        public string? PatientSurname { get; set; }
        public string UseStatus { get; set; } = null!;

        public DateTime? CheckupDate { get; set; }
        public int CountIsCheck { get; set; }

        public int CountPrivileges { get; set; }
    }


    public class AdminHos
    { 
        public int? id { get; set; }
        public string? username { get; set; }

        public string? password { get; set; }

    }
}
