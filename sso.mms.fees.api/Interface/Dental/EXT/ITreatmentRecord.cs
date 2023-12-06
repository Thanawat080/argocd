using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.Dental.EXT
{
    public interface ITreatmentRecord
    {
        Task<List<AaiDentalToothTypeM>> ToothList();
        Task<List<AaiDentalCheckHView>> TreatmentRecordList();
        Task<string> TreatmentCreate(AaiDentalCheckHView data);
        Task<string> DentalCheckupCreate(AaiDentalCheckDView data);
        Task<AaiDentalCheckHView> TreatmentRecordById(int id);

        Task<apiview850> api850(string personid);
        Task<List<AaiDentalCheckDView>> CheckDList(int id);


    }
}
