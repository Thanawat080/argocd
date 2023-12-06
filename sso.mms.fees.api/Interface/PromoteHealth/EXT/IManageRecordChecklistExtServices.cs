using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageRecordChecklistExtServices
    {
        Task<string> CheckLoginAdmin(string username, string password);

        Task<string> SaveRcord(saverecord data);

        Task<ManageRecordChecklistView> GetChecklistByCheckupId(decimal checkupId);

        Task<string> UpdateResult(ManageRecordChecklistView checklist);

        Task<string> UpdateRecord(saverecord data); 
    }
}
