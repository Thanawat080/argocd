using System.ComponentModel.DataAnnotations;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class CheckPermissionCheckListView
    {
        public decimal CheckupId { get; set; }
        public string? DeleteStatus { get; set; } = null!;
        public string? PatientName { get; set; }
        public string? PersonalId { get; set; }
        public string? Remark { get; set; }
        public string? PatientSurname { get; set; }
        public string? PatientSex { get; set; }

        public string? PatientTel { get; set; }
        [Required(ErrorMessage = "กรุณากรอกน้ำหนัก")]
        public string? PatientWeigth { get; set; }
        [Required(ErrorMessage = "กรุณากรอกส่วนสูง")]
        public string? PatientHeigth { get; set; }
        [Required(ErrorMessage = "กรุณากรอกความดัน")]
        public string? PatientPressure { get; set; }

        public DateTime? CheckupDate {  get; set; }

        public List<ChecklistM> checklistMs { get; set; }  = new List<ChecklistM> { };
        public decimal ChecklistPriceAll { get; set; }

        public int Allcount { get; set; }

        public string? Reson { get; set; }
        public string? UsernameAdmin { get; set; }

        public DateTime? ReadDate { get; set; }

        public bool? check850 { get; set; }

        public string? typeReserve { get; set; } = null;

        public decimal? PatientAge { get; set; }

        public bool CheckChecklistM { get; set; } = true;

        public bool default850 { get; set; } = false;

    }


    public class ChecklistM

        { 
        public decimal? Id { get; set; }
        public int No { get; set; }
        public decimal ChecklistId { get; set; }

        public string ChecklistName { get; set; } = null!;

        public bool statuscheck { get; set; }

        public decimal ChecklistPrice { get; set; }

        public string? ChecklistCode { get; set; }

        public bool? DefaultStatuscheck { get; set; }

        public string? ChecklistShortname { get; set; }
    }

    public class CheckPermissionToExcelView
    {
        public decimal CheckupId { get; set; }
        public string DeleteStatus { get; set; } = null!;
        public string? PatientName { get; set; }
        public string? PersonalId { get; set; }
        public string? Remark { get; set; }
        public string? PatientSurname { get; set; }
        public string? PatientSex { get; set; }
        public string? PatientTel { get; set; }
        public decimal ChecklistPriceAll { get; set; }
        public int Allcount { get; set; }
        public bool Hearing { get; set; }
        public bool Breast { get; set; }
        public bool Snelleneye { get; set; }
        public bool CBC { get; set; }
        public bool ChestXray { get; set; }
        public bool FBS { get; set; }
        public bool CR { get; set; }
        public bool LP { get; set; }
        public bool HBaAg { get; set; }
        public bool PAPSmear { get; set; }
        public bool Via { get; set; }
        public bool UA { get; set; }
        public bool FOBT { get; set; }
        public bool Eye1 { get; set; }

    }
    public class apiview850
    {
        public bool status { get; set; }

        public int age { get; set; }

        public string Name { get; set; }
        public string Sname { get; set; }
        public string Sex { get; set; }

        public string statusPerson { get; set; }

        public decimal price { get; set; }
        public string personalId { get; set;  }  
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

    }

    public class AuthenPerson
    { 
        public string? Adminusername { get; set; }
        public string? Type { get; set; }
        public person? Persons { get; set; } = new person();
    }
    public class person
    {

        public decimal CheckupId { get; set; }
        public string? PersonalId { get; set; }

        public string? PatientSex { get; set; }

        public string? PatientName { get; set; }
        public string? PatientSurname { get; set; }
        public string? PatientTel { get; set; }

        public string? Reson { get; set; }
        public string? UsernameAdmin { get; set; }

        public bool? check850 { get; set; } = true;

        public string? typeReserve { get; set; } = null;

        public bool default850 { get; set; } = false;
        public bool CheckChecklistM { get; set; } = true;
    }


}
