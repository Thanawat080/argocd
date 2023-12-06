using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Data;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;


namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IdpDbContext db;
        public SettingController(IdpDbContext idpDbContext)
        {
            this.db = idpDbContext;
        }

        [HttpGet("getstatussetting/{type}")]
        public async Task<ActionResult<CheckSettingModel>> GetStatusSetting(string type)
        {
            try
            {
                CheckSettingModel response = new CheckSettingModel();
                var res = db.SettingMs.Where(x => x.Type == type).FirstOrDefault();
                response.type = res.Type;
                response.isactive = (bool)res.Isactive;



                return response;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("gethosuserbyusername/{username}")]
        public async Task<ActionResult<HospitalUserM>> GetHosUserByUserName(string username)
        {
            try
            {



                return db.HospitalUserMs.Where(x => x.UserName == username && x.IsStatus == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }

}
