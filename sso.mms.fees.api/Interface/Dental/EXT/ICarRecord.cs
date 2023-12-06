using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
namespace sso.mms.fees.api.Interface.Dental.EXT
{
    public interface ICarRecord
    {
        Task<List<AaiDentalCarHView>> GetAll();
        Task<string> AddCarRecord(InsertDataViewModel carData);
        Task<string> UpdateCarHChangeDes(ChangeDesViewModel carHData);
    }
}
