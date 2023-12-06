using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Admin
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/admin")]
    public class PaymentOrderListController : ControllerBase
    {
        private readonly IPaymentOrderListService paymentServices;

        // GET: api/<AdminController>
        public PaymentOrderListController(IPaymentOrderListService paymentServices)
        {
            this.paymentServices = paymentServices;
        }

        [HttpGet("GetByWithdrawalNo")]
        public async Task<GetPaymentOrderList> GetByWithdrawalNo(string withdrawalNo)
        {
            try
            {
                var result = await paymentServices.GetByWithdrawalNo(withdrawalNo);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("GetHospByWitdraw")]
        public async Task<List<GetPaymentOrderList>> GetHospByWitdraw(string withdrawalNo)
        {
            try
            {
                var result = await paymentServices.GetHospByWitdraw(withdrawalNo);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetPaymentOList")]
        public async Task<List<GetPaymentOrderList>> GetPaymentOList()
        {
            try
            {
                var result = await paymentServices.GetPaymentOList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        [HttpGet("GetPersonPayorderSetNoBy")]
        public async Task<List<AaiHealthCheckupHViewModel>> GetPersonPayorderSetNoBy([FromQuery(Name = "payordersetno")] string payordersetno,
              [FromQuery(Name = "hoscode")] string? hoscode)
        {
            try
            {
                var result = await paymentServices.GetPersonPayorderSetNoBy(payordersetno, hoscode);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetPerson")]
        public async Task<List<AaiHealthCheckupHViewModel>> GetPerson(string withdrawalNo, string hospCode)
        {
            try
            {
                var result = await paymentServices.GetPerson(withdrawalNo, hospCode);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("SavePayorder")]
        public async Task<string> Save(SaveOrderT data)
        {
            try
            {
                var result = await paymentServices.Save(data);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("GetPayOrderHis")]
        public async Task<List<GetPayOrderListT>> GetPayOrderHis()
        {
            try
            {
                var result = await paymentServices.GetPayOrderHis();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        

        [HttpGet("GetPayOrder")]
        public async Task<List<GetPayOrderListT>> GetPayOrder()
        {
            try
            {
                var result = await paymentServices.GetPayOrder();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost("UpdatePayOrder")]
        public async Task<string> UpdatePayOrder(List<GetPayOrderListT> data)
        {
            try
            {
                var result = await paymentServices.UpdatePayOrder(data);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        
    }
}