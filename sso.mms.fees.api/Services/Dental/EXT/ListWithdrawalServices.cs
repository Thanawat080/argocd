using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;

namespace sso.mms.fees.api.Services.Dental.EXT
{
    public class ListWithdrawalServices : IListWithdrawals
    {
        private readonly DentalContext db;
        
        public ListWithdrawalServices(DentalContext dentalContext)
        {
            db = dentalContext;
        }
        public async Task<List<AaiDentalCheckH>> GetListWithdrawals()
        {
            return await db.AaiDentalCheckHs.Where(x => x.CheckStatus == "X" ||  x.CheckStatus == "E" || x.CheckStatus == "W").ToListAsync();
        }

    
    }
}
