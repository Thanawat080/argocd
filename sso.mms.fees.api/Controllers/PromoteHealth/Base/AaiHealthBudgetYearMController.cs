using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthBudgetYearMController : ControllerBase
    {
        private readonly IAaiHealthBudgetYearMBaseServices baseServices;

        // GET: api/<AdminController>

        public AaiHealthBudgetYearMController(IAaiHealthBudgetYearMBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        [HttpGet("GetAll")]
        public async Task<List<AaiHealthBudgetYearM>> Get()
        {
            List<AaiHealthBudgetYearM> result = await baseServices.GetAll();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GenerateBudgetYearM")]
        public async Task<string> ContabInsert()
        {
            var result = await baseServices.ContabInsert();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        

        [HttpGet("GetByBudYear/{year}")]
        public async Task<AaiHealthBudgetYearM> GetByBudYear(string year)
        {
            AaiHealthBudgetYearM result = await baseServices.GetByBudYear(year);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
