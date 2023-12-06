using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthCheckListDServices : IAaiHealthCheckListDBaseServices
    {
        private readonly PromoteHealthContext db;

        public AaiHealthCheckListDServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }
        public async Task<List<AaiHealthChecklistD>> GetByCheckListIdRawModel(int id)
        {
            try
            {
                var result = db.AaiHealthChecklistDs.Where(x => x.ChecklistId == id).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<string> Create(List<AaiHealthCheckListDView> data)
        {
            try
            {
                //create 
                foreach (var item1 in data)
                {
                    AaiHealthChecklistD checklistd = new AaiHealthChecklistD();
                    checklistd.ChecklistDtName = item1.ChecklistDtName;
                    checklistd.ChecklistDtStatus = (item1.ChecklistDtStatus ? "A" : "I");
                    checklistd.ChecklistId = item1.ChecklistId;
                    checklistd.IsOption = item1.IsOption;
                    checklistd.CreateDate = item1.CreateDate;
                    checklistd.CreateBy = item1.CreateBy;
                    checklistd.UpdateDate = item1.UpdateDate;
                    checklistd.UpdateBy = item1.UpdateBy;
                    await db.AaiHealthChecklistDs.AddAsync(checklistd);
                }
                db.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<List<AaiHealthCheckListDView>> GetByCheckListId(int id)
        {
            var result = db.AaiHealthChecklistDs.Where(x => x.ChecklistId == id).ToList().Select(x => new AaiHealthCheckListDView
            {
                ChecklistDtId = x.ChecklistDtId,
                ChecklistDtName = x.ChecklistDtName,
                ChecklistId = x.ChecklistId,
                IsOption = x.IsOption,
                ChecklistDtStatus = (x.ChecklistDtStatus ==  "A" ? true : false),
                CreateDate = DateTime.Now,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy
            }).ToList();

            return result;
        }

        public async Task<string> Update(List<AaiHealthCheckListDView> data)
        {
            try {
                //update 
                foreach (var item1 in data)
                {
                    AaiHealthChecklistD checklistd = new AaiHealthChecklistD();
                    checklistd = db.AaiHealthChecklistDs.Where(x => x.ChecklistDtId == item1.ChecklistDtId).FirstOrDefault();
                    checklistd.ChecklistDtName = item1.ChecklistDtName;
                    checklistd.ChecklistDtStatus = (item1.ChecklistDtStatus ? "A" : "I");
                    checklistd.UpdateDate = DateTime.Now;
                    checklistd.UpdateBy = "system";

                }
                db.SaveChanges();

                return "success"; 
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
            
        }
    }
}
