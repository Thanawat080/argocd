using Microsoft.AspNetCore.Mvc;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using Microsoft.EntityFrameworkCore;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.helper.ViewModels;

namespace sso.mms.portal.admin.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class SettingOpendataController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly UploadFileService uploadFileService;
        public SettingOpendataController(PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.uploadFileService = uploadFileService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> EditSettingOpendata(List<SettingOpendataT> SettingOpendata)
        {
            try
            {
                foreach (var item in SettingOpendata)
                {
                    var getSettingId = await db.SettingOpendataTs.FindAsync(item.Id);
                    if (getSettingId != null)
                    {
                        getSettingId.Title = item.Title;
                        getSettingId.Url = item.Url;
                        getSettingId.Detail = item.Detail;
                        getSettingId.ShowStatus = item.ShowStatus;
                        getSettingId.UploadImageName = item.UploadImageName;
                        getSettingId.UploadImagePath = item.UploadImagePath;
                        getSettingId.UpdateBy = item.UpdateBy;
                        getSettingId.UpdateDate =DateTime.Now;
                        db.SettingOpendataTs.Update(getSettingId);
                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        return Ok(new { status = false, message = "Data is Not Edit" });
                    }
                }
            return Ok(new { status = true, message = "Update Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }

           
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<SettingOpendataT>> GetSettingOpenDataT()
        {
            return await db.SettingOpendataTs.Where(w => w.IsActive == true).ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SettingOpendataT>> AddOpenDataT(SettingOpendataT OpenDataT)
        {
            try
            {

                await db.SettingOpendataTs.AddAsync(OpenDataT);
                await db.SaveChangesAsync();
                return Ok(new { status = true, message = "Message" });

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

            Uploadurl = $"{ConfigureCore.redirectsPortalAdmin}Files/";

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

