using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthSetRefDoctorController : ControllerBase
    {
        private readonly IAaiHealthSetRefDoctorBaseServices baseServices;
        public AaiHealthSetRefDoctorController(IAaiHealthSetRefDoctorBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        
        [HttpPost("create")]
        public async Task<string> Create(List<AaiHealthSetRefDoctorView> data)
        {
            var result = await baseServices.Create(data);

            return result;
        }

        [HttpPost("update")]
        public async Task<string> Update(List<AaiHealthSetRefDoctorView> data)
        {
            var result = await baseServices.Update(data);

            return result;
        }

        [HttpPost("delete")]
        public async Task<string> Delete(List<AaiHealthSetRefDoctorView> data)
        {
            var result = await baseServices.Delete(data);

            return result;
        }
    }
}
