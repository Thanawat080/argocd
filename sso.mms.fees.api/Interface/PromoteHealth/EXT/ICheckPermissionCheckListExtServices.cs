using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface ICheckPermissionCheckListExtServices
    {
        Task<List<CheckPermissionCheckListView>> CheckPerson(List<person> data);

        Task<apiview850> CheckPersonFrom850Api(string identificationnumber);
    }
}
