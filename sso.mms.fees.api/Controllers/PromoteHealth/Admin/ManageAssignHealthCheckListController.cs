using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Admin
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/admin")]
    public class ManageAssignHealthCheckListController : ControllerBase
    {
        private readonly IManageAssignHealthCheckListAdminServices adminServices;

        // GET: api/<AdminController>

        public ManageAssignHealthCheckListController(IManageAssignHealthCheckListAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }
        [HttpPost("ManageTypeCreate")]
        public async Task<string> ManageTypeCreate(ManageAssignHealthCheckListView data)
        {
            var result = await adminServices.ManageTypeCreate(data);

            return result;
        }

        [HttpPost("ManageTypeEdit")]
        public async Task<string> ManageTypeEdit(ManageAssignHealthCheckListView data)
        {
            var result = await adminServices.ManageTypeEdit(data);

            return result;
        }
    }
}
