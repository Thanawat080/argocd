using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Data;

namespace sso.mms.fees.api.Services.PromoteHealth.Admin
{
    public class ManageCallBackService : IManageCallBackService
    {
        private readonly IdpDbContext Idpdb;
        private readonly PromoteHealthContext db;
        private readonly HttpClient httpClient;
        public ManageCallBackService(PromoteHealthContext DbContext, HttpClient httpClient, IdpDbContext DbIdpContext)
        {
            this.db = DbContext;
            this.Idpdb = DbIdpContext;
            this.httpClient = httpClient;
        }
        public async Task<Response<string>> CallBackPromoteHealth(string PayOrderId, string SignBy)
        {
            Response<string> res = new Response<string>();

            try 
            {
                Console.WriteLine(PayOrderId);
                Console.WriteLine(SignBy);
                if (PayOrderId.EndsWith(","))
                {
                    PayOrderId = PayOrderId.Substring(0, PayOrderId.Length - 1);
                }
                string[] firstThird = PayOrderId.Split(',');
                foreach (var item in firstThird)
                {
                    AaiHealthPayOrderT aaiHealthPayOrderT = new AaiHealthPayOrderT();
                    AaiHealthPayOrderT res1 = db.AaiHealthPayOrderTs.Where(x => x.PayOrderId == Convert.ToDecimal(item)).FirstOrDefault();
                    res1.PayOrderStatus = "B";
                    res1.SignBy = SignBy;
                    res1.SignDate = DateTime.Now;
                    db.Update(res1);
                }

                db.SaveChanges();

                res.Status = 1;
                res.Message = "success";
                res.Result = null;
                return res;

            }
            catch (Exception e) 
            {
                res.Status = 0;
                res.Message = e.Message;
                res.Result = null;
                Console.WriteLine($"Error: {e.Message}");
                return res;

            }
        }
    }
}
