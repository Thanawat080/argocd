using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.login.ViewModels;
using sso.mms.portal.admin.ViewModels;
using System.Collections;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.portal.admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        public string? env = ConfigureCore.ConfigENV;
        public AdminController(IdpDbContext idpDbContext, PortalDbContext portalDbContext)
        {
            this.db = idpDbContext;
            this.portalDb = portalDbContext;
        }


        [HttpGet("getrolegroupdata")]
        public async Task<ActionResult<IEnumerable<RoleGroupM>>> getrolegroupdata()
        {
            return await db.RoleGroupMs.Where(w => w.IsStatus == 1)
                                       .GroupJoin(db.RoleGroupListTs, p => p.Id, pc => pc.RoleGroupMId, (p, pc) => new { p, pc })
                                       .Select(x => new RoleGroupM
                                       {
                                           Id = x.p.Id,
                                           Name = x.p.Name,
                                           IsActive = x.p.IsActive,
                                           IsStatus = x.p.IsStatus,
                                           CreateDate = x.p.CreateDate,
                                           CreateBy = x.p.CreateBy,
                                           UpdateDate = x.p.UpdateDate,
                                           UpdateBy = x.p.UpdateBy,
                                           RoleGroupListTs = x.p.RoleGroupListTs.Where(w => w.IsStatus == 1).ToList()
                                       }).ToListAsync();
        }

        [HttpGet("getrolemenudata")]
        public async Task<ActionResult<IEnumerable<RoleGroupM>>> getrolemenudata()
        {
            return await db.RoleGroupMs.Where(w => w.IsStatus == 1)
                                       .GroupJoin(db.RoleGroupListTs, p => p.Id, pc => pc.RoleGroupMId, (p, pc) => new { p, pc })
                                       .Select(x => new RoleGroupM
                                       {
                                           Id = x.p.Id,
                                           Name = x.p.Name,
                                           IsActive = x.p.IsActive,
                                           IsStatus = x.p.IsStatus,
                                           CreateDate = x.p.CreateDate,
                                           CreateBy = x.p.CreateBy,
                                           UpdateDate = x.p.UpdateDate,
                                           UpdateBy = x.p.UpdateBy,
                                           RoleGroupListTs = x.p.RoleGroupListTs.Where(w => w.IsStatus == 1).ToList()
                                       }).ToListAsync();
        }

        [HttpPost("addRoleMenu")]
        public async Task<IActionResult> addRoleMenu(RoleGroupMdata roleData)
        {
            try
            {
                RoleGroupM newRoleGroup = new RoleGroupM()
                {
                    Name = roleData.Name,
                    IsActive = roleData.IsActive,
                    IsStatus = roleData.IsStatus,
                    CreateBy = roleData.CreateBy,
                    UpdateBy = roleData.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                await db.RoleGroupMs.AddAsync(newRoleGroup);
                await db.SaveChangesAsync();
                return Ok(new { status = true, message = "success" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("GetSessionUserT")]
        public async Task<IEnumerable<ViewModelSessionUserT>> GetSessionUserT()
        {
            return await db.SessionUserTs
                .Include(x => x.SsoUserM)
                .Include(x => x.AuditorUserM)
                .Include(x => x.HospitalUserM)
                .Select(x => new ViewModelSessionUserT
                {
                Id = x.Id,
                HospitalUserMId = x.Id,
                SsoUserMId = x.SsoUserMId,
                AuditorUserMId = x.AuditorUserMId,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                ChannelLogin = x.ChannelLogin == null ? "" : x.ChannelLogin,
                IsStatus = x.IsStatus,
                FirstName = x.HospitalUserM.FirstName + x.AuditorUserM.FirstName + x.SsoUserM.FirstName,
                LastName = x.HospitalUserM.LastName + x.AuditorUserM.LastName + x.SsoUserM.LastName,
                AccessType = x.AccessType
            }).Where(x=>x.AccessType == "login").OrderByDescending(o => o.CreateDate).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<dynamic>>> getSsoUser(string realmGroup)
        {
            if(realmGroup == "sso-mms-auditor")
            {
                return await db.AuditorUserMs.Where(w => w.IsActive == true).ToListAsync();
            }else
            {
                return await db.SsoUserMs.Where(w => w.IsActive == true).ToListAsync();
            }
           
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<PositionM>>> getSsoPosition()
        {
            return await db.PositionMs.Where(w => w.IsActive == true).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DepartmentM>>> getSsoDepartment()
        {
            return await db.DepartmentMs.Where(w => w.IsActive == true).ToListAsync();
        }

    }
}
