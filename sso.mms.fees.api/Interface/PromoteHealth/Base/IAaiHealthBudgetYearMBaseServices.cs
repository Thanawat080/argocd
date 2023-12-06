using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthBudgetYearMBaseServices
    {
        Task<List<AaiHealthBudgetYearM>> GetAll();

        Task<AaiHealthBudgetYearM> GetByBudYear(string year);

        Task<string> ContabInsert();
    }
}
