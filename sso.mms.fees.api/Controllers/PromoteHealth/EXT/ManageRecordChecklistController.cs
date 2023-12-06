using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class ManageRecordChecklistController : ControllerBase
    {
        private readonly IManageRecordChecklistExtServices extService;

        public ManageRecordChecklistController(IManageRecordChecklistExtServices extService)
        {
            this.extService = extService;
        }

        [HttpGet("GetChecklistByCheckupId/{checkupId}")]
        public async Task<ManageRecordChecklistView> GetChecklistByCheckupId(decimal checkupId)
        { 
            var result = await extService.GetChecklistByCheckupId(checkupId);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("CheckLoginAdmin/{username}/{password}")]
        public async Task<string> CheckLoginAdmin(string username, string password)
        {
            var result = await extService.CheckLoginAdmin(username, password);

            return result;
        }


        [HttpPost("SaveRcord")]
        public async Task<string> SaveRcord(saverecord data)
        {

            var result = await extService.SaveRcord(data);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
            
        }

        [HttpPost("UpdateResult")]
        public async Task<string> UpdateResult(ManageRecordChecklistView checklist)
        {
            var result = await extService.UpdateResult(checklist);

            return result;
        }

        [HttpPost("UpdateRecord")]
        public async Task<string> UpdateRecord(saverecord data)
        {
            var result = await extService.UpdateRecord(data);

            return result;
        }
    }
}
