using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Services.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
  
        [ApiController]
        [Route("api/[area]/[controller]")]
        [Area("PromoteHealth/Ext")]
        public class ManageCheckListDController : ControllerBase
    {
        private readonly IManageCheckListD extService;

        public ManageCheckListDController(IManageCheckListD extService)
        {
            this.extService = extService;
        }
        [HttpGet("GetAll")]
        public async Task<List<CheckListDAndManageChecklistCfg>> GetCheckListD(decimal? checklistMId, string? hosCode)
        {
            try
            {
                
                List<CheckListDAndManageChecklistCfg> result = await extService.GetCheckListD(checklistMId, hosCode);

            if (result != null)
            {
                  
                 
                return result;
            }
            else
            {
                return null;
            }
            }catch (Exception ex)
            {
                return null;
            }
          
        }

        // GET: ManageCheckListDController

    }
}
