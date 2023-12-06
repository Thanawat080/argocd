using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthCheckListMController : ControllerBase
    {
        private readonly IAaiHealthCheckListMBaseServices baseServices;
        public AaiHealthCheckListMController(IAaiHealthCheckListMBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        [HttpGet("GetAll")]
        public async Task<List<AaiHealthChecklistM>> Get()
        {
            List<AaiHealthChecklistM> result = await baseServices.GetAll();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }


        [HttpGet("GetById/{id}")]
        public async Task<AaiHealthChecklistM> GetById(string id)
        {
            AaiHealthChecklistM result = await baseServices.GetById(Int32.Parse(id));

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetByYear/{year}")]
        public async Task<List<AaiHealthChecklistM>> GetByYear(int year)
        {
            var result = await baseServices.GetByYear(year);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpPost("update")]
        public async Task<string> Update(AaiHealthChecklistM data)
        {
            var result = await baseServices.Update(data);
            return result;
        }

        [HttpPost("create")]
        public async Task<decimal> Create(AaiHealthChecklistM data)
        {
            var result = await baseServices.Create(data);

            return result;
        }
    }
}
