using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.Pages;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.portal.admin.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace sso.mms.portal.admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingMController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        public string? env = ConfigureCore.ConfigENV;
        public SettingMController(IdpDbContext idpDbContext, PortalDbContext portalDbContext)
        {
            this.db = idpDbContext;
            this.portalDb = portalDbContext;
        }

        [HttpGet("getsetting")]
        public async Task<ActionResult<List<SettingM>>> getsetting()
        {
            return db.SettingMs.ToList();
        }


        [HttpPost("savesetting")]
        public async Task<ActionResult> savesetting(List<SettingM> data)
        {
            try
            {
                foreach(var setting in data)
                {
                    db.SettingMs.Update(setting);
                    db.SaveChanges();
                }

                

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<CheckSettingModel>>> getstatussetting(string type)
        {
            try
            {
                List<CheckSettingModel> listres = new List<CheckSettingModel>();
                string[] textList = type.Split(",");
                 
                foreach (var item in textList)
                {
                    CheckSettingModel resget = new CheckSettingModel();
                    var res = db.SettingMs.Where(x => x.Type == item).FirstOrDefault();
                    if (res != null) {
                        resget.type = res.Type;
                        resget.isactive = (bool)res.Isactive;
                        listres.Add(resget);
                    }
                }



                return listres;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }
    }
}
