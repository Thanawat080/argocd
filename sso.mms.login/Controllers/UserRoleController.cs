using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sso.mms.login.ViewModels.KeyCloak;
using sso.mms.login.ViewModels;
using sso.mms.helper.Data;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        public UserRoleController(IdpDbContext db, PortalDbContext portalDb)
        {
            this.db = db;
            this.portalDb = portalDb;
        }
        [HttpGet("[action]/{username}")]
        //Task<ActionResult<IEnumerable<ResponseShortToken>>>
        public async Task<ActionResult<UserRole>> GetUserRole(string username)
        {
            try
            {
                var roleUserJoin = await db.RoleUserMappings
                .GroupJoin(db.RoleGroupMs, rum => rum.RoleGroupId, rgm => rgm.Id, 
                (rum, rgmGroup) => new { Rum = rum, RgmGroup = rgmGroup })
                .SelectMany(x => x.RgmGroup.DefaultIfEmpty(), (x, rgm) => new { x.Rum, Rgm = rgm })
                .GroupJoin(db.RoleGroupListTs, combinedEntry => combinedEntry.Rgm.Id, rglt => rglt.RoleGroupMId, 
                (combinedEntry, rglt) => new { CombinedEntry = combinedEntry, Rglt = rglt })
                .SelectMany(x => x.Rglt.DefaultIfEmpty(), (x, rglt) => new { x.CombinedEntry.Rum, x.CombinedEntry.Rgm, Rglt = rglt })
                .GroupJoin(db.RoleMenuMs, combinedEntry => combinedEntry.Rglt.RoleMenuMId, rmm => rmm.Id, 
                (combinedEntry, rmm) => new { CombinedEntry = combinedEntry, Rmm = rmm })
                .Where(x => x.CombinedEntry.Rgm.IsStatus == 1)
                .SelectMany(x => x.Rmm.DefaultIfEmpty(), (x, rmm) => new 
                {
                    username = x.CombinedEntry.Rum.UserName,
                    roleCode = x.CombinedEntry.Rgm.RoleCode,
                    menuCode = rmm.MenuCode,
                    appCode = rmm.AppCode,
                    isRoleRead = x.CombinedEntry.Rglt.IsRoleRead,
                    isRoleCreate = x.CombinedEntry.Rglt.IsRoleCreate,
                    isRoleUpdate = x.CombinedEntry.Rglt.IsRoleUpdate,
                    isRoleDelete = x.CombinedEntry.Rglt.IsRoleDelete,
                    isRolePrint = x.CombinedEntry.Rglt.IsRolePrint,
                    isRoleApprove = x.CombinedEntry.Rglt.IsRoleApprove,
                    isRoleCancle = x.CombinedEntry.Rglt.IsRoleCancel
                }).Where(fullEntry => fullEntry.username == username)
                .ToListAsync();

                

                UserRole result = roleUserJoin.GroupBy(item => item.username).Select(group => new UserRole
                {
                    username = group.Key,
                    role = group.GroupBy(item => item.roleCode).Select
                    (
                        group => new RoleList
                        {
                            roleCode = group.Key,
                            menu = group.GroupBy(item => item.menuCode).Select
                            (
                                menuGroup => new MenuPerMit
                                {
                                    menuCode = menuGroup.Key,
                                    appCode = menuGroup.First().appCode,
                                    isRoleRead = menuGroup.First().isRoleRead,
                                    isRoleCreate = menuGroup.First().isRoleCreate,
                                    isRoleUpdate = menuGroup.First().isRoleUpdate,
                                    isRoleDelete = menuGroup.First().isRoleDelete,
                                    isRolePrint = menuGroup.First().isRolePrint,
                                    isRoleApprove = menuGroup.First().isRoleApprove,
                                    isRoleCancle = menuGroup.First().isRoleCancle
                                }
                            ).ToList()
                        }
                    ).ToList()
                }).FirstOrDefault();

                return result;
                
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("[action]/{username}")]
        //Task<ActionResult<IEnumerable<ResponseShortToken>>>
        public async Task<ActionResult<string>> GetOrgCode(string username)
        {
            try
            {
                var hosId = db.HospitalUserMs.FirstOrDefault(hum => hum.UserName == username).HospitalMId;
                if(hosId == null )
                {
                    return null;
                }
                var res = portalDb.HospitalMs.FirstOrDefault(hm => hm.Id == hosId).Code;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        [HttpPost("insertRoleUserMapping")]
        public async Task<ActionResult> insertRoleUserMapping(RoleUserMappingModel data)
        {
            try
            {
                RoleUserMapping datasavae = new RoleUserMapping();
                datasavae.UserName = data.UserName;
                datasavae.RoleGroupId = data.RoleGroupId;
                datasavae.UserType = data.UserType;
                datasavae.UpdateBy = data.UpdateBy;
                datasavae.CreateBy = data.CreateBy;

                await db.RoleUserMappings.AddAsync(datasavae);
                await db.SaveChangesAsync();

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = "cannot insert " + ex.Message });
            }
        }
    }
}
