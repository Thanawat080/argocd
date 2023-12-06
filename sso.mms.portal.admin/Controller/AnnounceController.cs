using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using System.Net.NetworkInformation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.portal.admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnounceController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly UploadFileService uploadFileService;

        public AnnounceController(PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.uploadFileService = uploadFileService;
        }

        // GET: api/<AnnounceController>
        [HttpGet("GetAnnounceT")]
        public async Task<IEnumerable<AnnounceT>> GetAnnounceT()
        {
            return await db.AnnounceTs.Select(e => new AnnounceT
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                IsActive = e.IsActive,
                IsStatus = e.IsStatus,
                ImagePath = e.ImagePath,
                ImageFile = e.ImageFile,
                ActiveStatus = e.ActiveStatus,
            }).ToListAsync();
        }

        // POST api/<AnnounceController>

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertAnnounceT(AnnounceT announcement)
        {
            try
            {
                // ทำการบันทึกข้อมูลประกาศลงในฐานข้อมูล
                db.AnnounceTs.Add(announcement);
                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> EditAnnounce(AnnounceT request)
        {

            var result = await db.AnnounceTs.Where(w => w.IsActive == true && w.Id == request.Id).FirstOrDefaultAsync();
            try
            {
                    if (result != null)
                    {

                        result.Title = request.Title;
                        result.StartDate = request.StartDate;
                        result.EndDate = request.EndDate;
                        result.IsActive = request.IsActive;
                        result.IsStatus = request.IsStatus;
                        result.CreateDate = request.CreateDate;
                        result.CreateBy = request.CreateBy;
                        result.UpdateDate = request.UpdateDate;
                        result.UpdateBy = request.UpdateBy;
                        result.ImagePath = request.ImagePath;
                        result.ImageFile = request.ImageFile;
                        result.ActiveStatus = request.ActiveStatus;
                       
                        db.AnnounceTs.Update(result);
                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        return Ok(new { status = false, message = "Data is Not Edit" });
                    }
                
                return Ok(new { status = true, message = "Update Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }


        }
    }
}
