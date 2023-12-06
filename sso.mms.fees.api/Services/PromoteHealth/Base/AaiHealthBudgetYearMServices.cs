using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthBudgetYearMServices : IAaiHealthBudgetYearMBaseServices
    {
        private readonly PromoteHealthContext db;

        public AaiHealthBudgetYearMServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<string> ContabInsert()
        {
            try
            {
                var res = db.AaiHealthBudgetYearMs.Where(x => x.BudgetYear == DateTime.Now.AddYears(+543).Year.ToString()).Count();
                if (res == 0)
                {
                    AaiHealthBudgetYearM aaiHealthBudgetYearM = new AaiHealthBudgetYearM();
                    aaiHealthBudgetYearM.BudgetYear = DateTime.Now.AddYears(+543).Year.ToString();
                    aaiHealthBudgetYearM.BudgetYearStatus = true;
                    aaiHealthBudgetYearM.UpdateDate = DateTime.Now;
                    aaiHealthBudgetYearM.CreateDate = DateTime.Now;
                    aaiHealthBudgetYearM.CreateBy = "SYSTEM AUTO GEN";
                    aaiHealthBudgetYearM.UpdateBy = "SYSTEM";
                }
                return "success";
            }
            catch (Exception ex) { return ex.Message; }
        }

        public async Task<List<AaiHealthBudgetYearM>> GetAll()
        {
            var result =  db.AaiHealthBudgetYearMs.ToList();
            return result;
        }

        public async Task<AaiHealthBudgetYearM> GetByBudYear(string year)
        {
            var result = db.AaiHealthBudgetYearMs.Where(x => x.BudgetYear == year).FirstOrDefault();
            return result;
        }


    }
}
