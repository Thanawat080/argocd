using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthReserveHServices : IAaiHealthReserveHBaseServices
    {

        private readonly PromoteHealthContext db;

        public AaiHealthReserveHServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<string> UpdateReserveStatus(decimal reserveId, string status)
        {
            try
            {
                //update 
                var reserveH = db.AaiHealthReserveHs.Where(x => x.ReserveId == reserveId).FirstOrDefault();
                reserveH.DocStatus = status;
                if(status == "13")
                {
                    var checkupH = db.AaiHealthCheckupHs.Where(x => x.ReserveId == reserveId).ToList();
                    foreach (var data in checkupH)
                    {
                        data.UseStatus = "C";
                    }
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
