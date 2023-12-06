using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IGetReserveHExtServices
    {
        Task<List<GetReServeHView>> GetAll(string hoscode);

        Task<ManageBookHealthCheckupView> GetReserveHById(int reserveId);
    }
}
