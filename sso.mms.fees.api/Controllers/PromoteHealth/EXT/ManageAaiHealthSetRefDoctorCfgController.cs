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
        public class ManageAaiHealthSetRefDoctorCfgController : ControllerBase
    {
            private readonly IManageAaiHealthSetRefDoctorCfg extService;

        public ManageAaiHealthSetRefDoctorCfgController(IManageAaiHealthSetRefDoctorCfg extService)
        {
            this.extService = extService;
        }
        [HttpGet("GetAll")]
        public async Task<List<AaiHealthSetRefDoctorMView>> GetDoctor(decimal? checklistMId, string? hosCode)
        {
            try
            {
                
                List<AaiHealthSetRefDoctorMView> result = await extService.GetDoctor(checklistMId, hosCode);

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
