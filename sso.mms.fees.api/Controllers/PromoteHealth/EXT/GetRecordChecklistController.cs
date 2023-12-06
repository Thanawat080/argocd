using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class GetRecordChecklistController : ControllerBase
    {
        private readonly IGetRecordChecklistExtServices extService;

        public GetRecordChecklistController(IGetRecordChecklistExtServices extService)
        {
            this.extService = extService;
        }

        [HttpGet("GetAllRecordChecklist/{hoscode}")]
        public async Task<List<GetReServeHView>> GetAllRecordChecklist(string hoscode)
        {
            List<GetReServeHView> result = await extService.GetAllRecordChecklist(hoscode);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetAllRecordChecklistByUser/{hoscode}")]
        public async Task<List<GetRecordChecklistView>> GetAllRecordChecklistByUser(string hoscode)
        {
            List<GetRecordChecklistView> result = await extService.GetAllRecordChecklistByUser(hoscode);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }


        [HttpGet("GetAllRoleAdminByHosCode/{hoscode}")]
        public async Task<List<AdminHos>> GetAllRoleAdminByHosCode(string hoscode)
        {
            List<AdminHos> result = await extService.GetAllRoleAdminByHosCode(hoscode);

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
