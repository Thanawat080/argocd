using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthCheckListDBaseServices
    {
        Task<List<AaiHealthChecklistD>> GetByCheckListIdRawModel(int id);
        Task<List<AaiHealthCheckListDView>> GetByCheckListId(int id);
        Task<string> Update(List<AaiHealthCheckListDView> data);
        Task<string> Create(List<AaiHealthCheckListDView> data);
    }
}
