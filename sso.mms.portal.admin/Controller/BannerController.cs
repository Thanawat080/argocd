using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using sso.mms.helper.PortalModel;
using System.Xml.Linq;
using sso.mms.notification.ViewModel.Response;
using sso.mms.helper.Services;
using sso.mms.notification.ViewModel;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.helper.Configs;
using sso.mms.helper.ViewModels;
using sso.mms.portal.admin.ViewModels;

namespace sso.mms.portal.admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly UploadFileService uploadFileService;
        public BannerController(PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.uploadFileService = uploadFileService;
        }
        //.Where(w=>w.StatusAnnounce == 1)
        [HttpGet("GetBannersT")]
        public async Task<IEnumerable<BannerT>> GetBannersT()
        {
            return await db.BannerTs.Select(e => new BannerT
            {
                Id = e.Id,
                BannerName = e.BannerName,
                CreateDate = e.CreateDate,
                UploadImageName = e.UploadImageName,
                UploadImagePath = e.UploadImagePath,
                StatusAnnounce = e.StatusAnnounce,

            }).OrderByDescending(e => e.Id).ToListAsync();
        }

        [HttpGet("GetPortalBannersT")]
        public async Task<IEnumerable<BannerT>> GetPortalBannersT()
        {
            return await db.BannerTs.Select(e => new BannerT
            {
                Id = e.Id,
                BannerName = e.BannerName,
                CreateDate = e.CreateDate,
                UploadImageName = e.UploadImageName,
                UploadImagePath = e.UploadImagePath,
                StatusAnnounce = e.StatusAnnounce,
                Url = e.Url,
            }).OrderByDescending(e => e.Id).Where(w=>w.StatusAnnounce == 1).ToListAsync();
        }

        [HttpGet("GetBannerT/{id}")]
        public async Task<ActionResult<BannerT?>> GetBannerT(int Id)
        {
            //var query = await db.BannerTs.Where(x => x.Id == Id).ToListAsync();
                return await db.BannerTs.FirstOrDefaultAsync(x => x.Id == Id);
        }

        [HttpPost("AddBannerT")]
        public async Task<ActionResult<BannerT>> AddBannerT(BannerT bannerT)
        {
            try
            {
                BannerT banner = new BannerT()
                {
                    BannerName = bannerT.BannerName,
                    Url = bannerT.Url,
                    CreateDate = bannerT.CreateDate,
                    StatusAnnounce = bannerT.StatusAnnounce,
                    UploadImagePath = bannerT.UploadImagePath,
                    IsActive = true,
                    IsStatus = 1,
                    CreateBy = "",
                    UpdateDate = DateTime.Now,
                    UpdateBy = ""
                };
                await db.BannerTs.AddAsync(banner);
                await db.SaveChangesAsync();

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpPost("EditBannerT")]
        public async Task<ActionResult<BannerT>> EditBannerT(BannerT bannerT)
        {

            var getBannerById = await db.BannerTs.FindAsync(bannerT.Id);

            try
            {
                if (getBannerById != null)
                {
                    //getBannerById.BannerName = bannerT.BannerName;
                    //getBannerById.Url = bannerT.Url;
                    getBannerById.StatusAnnounce = bannerT.StatusAnnounce;
                    //getBannerById.UploadImagePath = bannerT.UploadImagePath;
                    getBannerById.IsActive = true;
                    getBannerById.IsStatus = 1;
                    getBannerById.CreateBy = "";
                    //getBannerById.CreateDate = bannerT.CreateDate;
                    getBannerById.UpdateDate = DateTime.Now;
                    getBannerById.UpdateBy = "";

                    db.BannerTs.Update(getBannerById);
                    await db.SaveChangesAsync();
                    return Ok(new { status = true, message = "success" });
                }
                else
                {
                    return Ok(new { status = false, message = "No Data in Database!" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<ResponseUpload> UploadBanner([FromForm] List<IFormFile>? file)
        {
            Console.WriteLine("file",file);
            string Uploadurl;
            Uploadurl = $"{ConfigureCore.redirectsPortalAdmin}Files/";
            Console.WriteLine(Uploadurl);

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
