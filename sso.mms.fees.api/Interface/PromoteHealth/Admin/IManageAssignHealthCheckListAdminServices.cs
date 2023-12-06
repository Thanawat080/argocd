using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Admin
{
    public interface IManageAssignHealthCheckListAdminServices
    {
        Task<string> ManageTypeEdit(ManageAssignHealthCheckListView data);

        Task<string> ManageTypeCreate(ManageAssignHealthCheckListView data);
    }
}
