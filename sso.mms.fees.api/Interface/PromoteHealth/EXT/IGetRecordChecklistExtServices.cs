using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IGetRecordChecklistExtServices
    {
        Task<List<GetReServeHView>> GetAllRecordChecklist(string hoscode);

        Task<List<GetRecordChecklistView>> GetAllRecordChecklistByUser(string hoscode);

        Task<List<AdminHos>> GetAllRoleAdminByHosCode(string hoscode);

    }
}
