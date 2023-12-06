using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class CheckPermissionCheckListController : ControllerBase
    {
        private readonly ICheckPermissionCheckListExtServices extService;

        public CheckPermissionCheckListController(ICheckPermissionCheckListExtServices extService)
        {
            this.extService = extService;
        }
        [HttpPost("CheckPermission")]

        public async Task<List<CheckPermissionCheckListView>> CheckPerson(List<person> data)
        {
            List<CheckPermissionCheckListView> result = await extService.CheckPerson(data);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
            
        }

        [HttpGet("CheckPersonFrom850Api")]

        public async Task<apiview850> CheckPersonFrom850Api(string identificationnumber)
        {
            apiview850 result = await extService.CheckPersonFrom850Api(identificationnumber);

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
