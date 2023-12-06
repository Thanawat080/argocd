using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAlreadyCheckListBaseServices
    {
        Task<bool> Already_CheckList(string PERSONAL_ID, string? YearNow, string CheckListName);
        Task<apiview850> api850(string personid);
        Task<bool> Already_CheckList2(string PERSONAL_ID, string? YearNow, string CheckListName, int Y);
    }
}
