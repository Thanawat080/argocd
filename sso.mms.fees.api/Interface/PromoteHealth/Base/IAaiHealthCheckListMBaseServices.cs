using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthCheckListMBaseServices
    {
        Task<List<AaiHealthChecklistM>> GetAll();
        Task<AaiHealthChecklistM> GetById(int id);
        Task<List<AaiHealthChecklistM>> GetByYear(int year);
        Task<String> Update(AaiHealthChecklistM data);
        Task<decimal> Create(AaiHealthChecklistM data);
    }
}
