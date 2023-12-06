using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthCheckListMServices : IAaiHealthCheckListMBaseServices
    {

        private readonly PromoteHealthContext db;

        public AaiHealthCheckListMServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<List<AaiHealthChecklistM>> GetAll()
        {
            var result = db.AaiHealthChecklistMs.ToList();
            return result;
        }


        public async Task<AaiHealthChecklistM> GetById(int id)
        {
            var result = db.AaiHealthChecklistMs.Where(x => x.ChecklistId == id).FirstOrDefault();
            return result;
        }

        public async Task<List<AaiHealthChecklistM>> GetByYear(int year)
        {
            var result = db.AaiHealthChecklistMs.Where(x => x.BudgetYear == year.ToString()).ToList();
            return result;
        }

        public async Task<string> Update(AaiHealthChecklistM data)
        {
            try
            {
                //update 

                AaiHealthChecklistM checklistd = new AaiHealthChecklistM();
                checklistd = db.AaiHealthChecklistMs.Where(x => x.ChecklistId == data.ChecklistId).FirstOrDefault();
                checklistd.ChecklistName = data.ChecklistName;
                checklistd.ChecklistPrice = data.ChecklistPrice;
                checklistd.ChecklistStatus = data.ChecklistStatus;
                checklistd.UpdateDate = DateTime.Now;
                checklistd.UpdateBy = "system";
                db.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        public async Task<decimal> Create(AaiHealthChecklistM data)
        {
            try
            {

                data.UpdateDate = DateTime.Now;
                data.UpdateBy = "system";
                data.CreateDate = DateTime.Now;
                data.CreateBy = "system";
                await db.AaiHealthChecklistMs.AddAsync(data);
                db.SaveChanges();
                decimal checklistid = data.ChecklistId;
                if (checklistid != 0)
                {
                    return checklistid;
                }
                else {
                    return 0;
                }
                
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

    }
}
