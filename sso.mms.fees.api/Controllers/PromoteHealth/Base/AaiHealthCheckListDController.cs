using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Collections.Generic;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthCheckListDController : ControllerBase
    {
        private readonly IAaiHealthCheckListDBaseServices baseServices;
        public AaiHealthCheckListDController(IAaiHealthCheckListDBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        [HttpGet("GetByCheckListIdRawModel/{id}")]
        public async Task<List<AaiHealthChecklistD>> GetByCheckListIdRawModel(string id)
        {
            List<AaiHealthChecklistD> result = await baseServices.GetByCheckListIdRawModel(Int32.Parse(id));

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetByCheckListId/{id}")]
        public async Task<List<AaiHealthCheckListDView>> GetByCheckListId(string id)
        {
            List<AaiHealthCheckListDView> result = await baseServices.GetByCheckListId(Int32.Parse(id));

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
        public async Task<string> Update(List<AaiHealthCheckListDView> data)
        {
            var result = await baseServices.Update(data);
            return result;
        }


        [HttpPost("create")]
        public async Task<string> Create(List<AaiHealthCheckListDView> data)
        {
            var result = await baseServices.Create(data);

            return result;
        }
    }
}
