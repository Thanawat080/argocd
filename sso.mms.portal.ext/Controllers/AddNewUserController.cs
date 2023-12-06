using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using sso.mms.portal.ext.ViewModels;
using sso.mms.helper.Data;
using sso.mms.notification.ViewModel;


using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace sso.mms.portal.ext.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddNewUserController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        public string? env = ConfigureCore.ConfigENV;
        private readonly UploadFileService uploadFileService;
        public AddNewUserController(IdpDbContext idpDbContext, PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = idpDbContext;
            this.portalDb = portalDbContext;
            this.uploadFileService = uploadFileService;
        }


        [HttpGet("getHospitalNameByCode/{code}")]
        public async Task<ActionResult<string>> getHospitalNameByCode(string code)
        {
            try
            {
                return portalDb.HospitalMs.Where(x => x.Code == code).FirstOrDefault().Name;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }




        [HttpGet("gethospitaluserbyid/{iduser}")]
        public async Task<ActionResult<HospitalUserM>> GetHospitalUserById(int iduser)
        {
            return db.HospitalUserMs.Where(x => x.Id == iduser).FirstOrDefault();
        }

        [HttpGet("gethospitaluserbyusername/{username}")]
        public async Task<ActionResult<HospitalUserM>> GetHospitalUserByUsername(string username)
        {
            return db.HospitalUserMs.Where(x => x.UserName == username).FirstOrDefault();
        }



        [HttpPost("updatehospitaluser")]
        public async Task<ActionResult> editRoleGrouplist(HospitalUserM data)
        {

            try
            {
                DateTime currentDateTime = DateTime.Now;
                data.UpdateDate = currentDateTime;
                db.HospitalUserMs.Update(data);
                db.SaveChanges();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("getUserbyTypeHosId/{hosid}/{adminid}")]
        public async Task<ActionResult<List<HospitalUserM>>> GetUserbyTypeHos(int hosid, int adminid)
        {
            try
            {

                return db.HospitalUserMs.Where(x => x.HospitalMId == hosid && x.Id != adminid && x.IsStatus != 2).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("getUserbyTypeHosIdAll/{hosid}/{adminid}")]
        public async Task<ActionResult<List<HospitalUserM>>> GetUserbyTypeHosAll(int hosid, int adminid)
        {
            try
            {

                return db.HospitalUserMs.Where(x => x.HospitalMId == hosid && x.Id != adminid).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("getrolegroupmbytype/{text}")]
        public async Task<ActionResult<List<RoleGroupM>>> GetRoleGroupByType(string text)
        {
            try
            {

                return db.RoleGroupMs.Where(x => x.UserGroup == text).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpPost("insertroleusermapping")]
        public async Task<ActionResult>InsertRoleUserMapping(ViewModelMapRoleUserMApping item)
        {
            try
            {
                RoleUserMapping item1 = new RoleUserMapping();
                item1.RoleGroupId = item.RoleGroupId;
                item1.UserName = item.UserName;
                item1.UpdateBy = item.UpdateBy;
                item1.CreateBy = item.CreateBy;
                item1.IsActive = item.IsActive;
                item1.UserType = item.UserType;

                await db.RoleUserMappings.AddAsync(item1);
                db.SaveChanges();
                return Ok(new { status = false, message = "success" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("deleteroleusermapping/{username}")]
        public async Task<ActionResult>DeleteRoleUserMapping(string UserName)
        {
            try
            {

                var rglist = db.RoleUserMappings.Where(x => x.UserName == UserName).ToList();
                foreach (var rg in rglist)
                {
                    db.RoleUserMappings.Remove(rg);
                    db.SaveChanges();
                }
                return Ok(new { status = false, message = "success" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        [HttpGet("getroleusermapping/{username}")]
        public async Task<ActionResult<List<RoleUserMapping>>> GetRoleUserMapping(string username)
        {
            try
            {

                return db.RoleUserMappings.Where(x => x.UserName == username).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("gethospitalcode/{hospitalid}")]
        public async Task<ActionResult<HospitalM>> GetHospitalCode(int hospitalid)
        {
            try
            {

                return portalDb.HospitalMs.Where(x => x.Id == hospitalid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpPost("addnewhosuser")]
        public async Task<ActionResult> AddNewHosUser(ViewModelMapRoleUserMApping item)
        {
            try
            {
                RoleUserMapping item1 = new RoleUserMapping();
                item1.RoleGroupId = item.RoleGroupId;
                item1.UserName = item.UserName;
                item1.UpdateBy = item.UpdateBy;
                item1.CreateBy = item.CreateBy;
                item1.IsActive = item.IsActive;
                item1.UserType = item.UserType;

                await db.RoleUserMappings.AddAsync(item1);
                db.SaveChanges();
                return Ok(new { status = false, message = "success" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet("gethospitaluserbyusernamelist/{username}/{hospitalid}")]
        public async Task<ActionResult<List<HospitalUserM>>> GetHospitalUserByUsernameList(string username, int hospitalid)
        {
            try
            {

                return db.HospitalUserMs.Where(x => x.UserName == username && x.HospitalMId == hospitalid && x.IsStatus != 2).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }



        [HttpGet("changestautsdelete/{iduser}")]
        public async Task<ActionResult> ChangeGetStatusDelete(int iduser)
        {
            try
            {
                HospitalUserM data = db.HospitalUserMs.Where(x => x.Id == iduser).FirstOrDefault();
                DateTime currentDateTime = DateTime.Now;
                data.UpdateDate = currentDateTime;
                data.IsStatus = 2;
                db.HospitalUserMs.Update(data);
                db.SaveChanges();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet("getprefix")]
        public async Task<ActionResult<List<PrefixM>>> GetPrefix()
        {
            try
            {
                return db.PrefixMs.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]/{username}/{hospitalid}")]
        public async Task<ActionResult<List<HospitalUserM>>> getHospitalUserByUsernameListAll(string username, int hospitalid)
        {
            try
            {

                return db.HospitalUserMs.Where(x => x.UserName == username && x.HospitalMId == hospitalid).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpPost("[action]")]
        public async Task<ResponseUpload> UploadImage([FromForm] List<IFormFile>? file)
        {
            string Uploadurl;

            Uploadurl = $"{ConfigureCore.redirectPortalExt}Files/";

            (string errorMessage, string UploadImagePath) = await uploadFileService.UploadImage(file,"");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                var fail = new ResponseUpload
                {
                    FileName = "",
                    Path_Url = "",
                    Error = errorMessage
                };
                return fail;
            }
            if (String.IsNullOrEmpty(UploadImagePath))
            {
                var fail = new ResponseUpload
                {
                    FileName = "",
                    Path_Url = "",
                    Error = "Cannot Upload"
                };
                return fail;
            }
            var success = new ResponseUpload
            {
                FileName = UploadImagePath,
                Path_Url = Uploadurl + UploadImagePath,
            };

            return success;
        }

    }
}
