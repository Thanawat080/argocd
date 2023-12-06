using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Admin
{

        public interface IPaymentOrderListService
        {
        Task<List<GetPaymentOrderList>> GetPaymentOList();
        Task<List<AaiHealthCheckupHViewModel>> GetPerson(string withdrawalNo, string hospCode);
        Task<GetPaymentOrderList> GetByWithdrawalNo(string withdrawalNo);
        Task<List<GetPaymentOrderList>> GetHospByWitdraw(string withdrawalNo);

        Task<string> Save(SaveOrderT data);

        Task<List<GetPayOrderListT>> GetPayOrder();
        Task<List<GetPayOrderListT>> GetPayOrderHis();

        Task<List<AaiHealthCheckupHViewModel>> GetPersonPayorderSetNoBy(string payordersetno, string? hoscode);
        Task<string> UpdatePayOrder(List<GetPayOrderListT> data);


    };
    
}
