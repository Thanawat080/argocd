using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.portal.admin.Pages;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.portal.admin.ViewModels;
using System.Net;

namespace sso.mms.portal.admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageMentMenuController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        public string? env = ConfigureCore.ConfigENV;
        public ManageMentMenuController(IdpDbContext idpDbContext, PortalDbContext portalDbContext)
        {
            this.db = idpDbContext;
            this.portalDb = portalDbContext;
        }


        [HttpGet("getmenu")]
        public async Task<ActionResult<List<RoleAppM>>> getmenu()
        {
            return db.RoleAppMs.ToList();
        }


        [HttpGet("getmenueditbycode/{code}")]
        public async Task<ActionResult<List<RoleMenuM>>> getmenueditbycode(string code)
        {
            return db.RoleMenuMs.Where(x => x.AppCode == code).ToList();
        }

        [HttpGet("getappmenu/{id}")]
        public async Task<ActionResult<RoleAppM>> getappmenu(int id)
        {
            return db.RoleAppMs.Where(x => x.Id == id).FirstOrDefault();
        }


        [HttpPost("updateApp")]
        public async Task<ActionResult<RoleAppM>> getappmenu(RoleAppM data)
        {
            try
            {

                db.RoleAppMs.Update(data);
                db.SaveChanges();


                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("getUserbyTypeSSO")]
        public async Task<ActionResult<List<SsoUserM>>> getUserbyTypeSSO(string? text, int? length)
        {
            try
            {
                if(text is null && length == null) 
                {
                    return db.SsoUserMs.Where(x => x.IsStatus != 2).Take(100).ToList();
                }
                else
                {
                    if (length == null)
                    {
                        if (text is not null)
                        {
                            return db.SsoUserMs.Where(q => q.UserName.ToLower().Contains(text.ToLower()) ||
                                    q.FirstName.ToLower().Contains(text.ToLower()) ||
                                    q.LastName.ToLower().Contains(text.ToLower())).Take(100).ToList();
                        }
                        else
                        {
                            return db.SsoUserMs.Where(x => x.IsStatus != 2).Take(100).ToList();
                        }
                            
                    }
                    else
                    {
                        if(length == -1)
                        {
                            if(text is not null)
                            {
                                return db.SsoUserMs.Where(q => q.UserName.ToLower().Contains(text.ToLower()) ||
                                    q.FirstName.ToLower().Contains(text.ToLower()) ||
                                    q.LastName.ToLower().Contains(text.ToLower())).ToList();
                            }
                            else
                            {
                                return db.SsoUserMs.Where(x => x.IsStatus != 2).ToList();
                            }
                        }
                        else
                        {
                            int maxLength = Convert.ToInt32(length);
                            if (text is not null)
                            {
                                return db.SsoUserMs.Where(q => q.UserName.ToLower().Contains(text.ToLower()) ||
                                    q.FirstName.ToLower().Contains(text.ToLower()) ||
                                    q.LastName.ToLower().Contains(text.ToLower())).Take(maxLength).ToList();
                            }
                            else
                            {
                                return db.SsoUserMs.Where(x => x.IsStatus != 2).Take(maxLength).ToList();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("getUserbyTypeHos")]
        public async Task<ActionResult<List<HospitalUserM>>> getUserbyTypeHos()
        {
            try
            {

                return db.HospitalUserMs.Where(x => x.IsStatus != 2).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("getUserbyTypeAudit")]
        public async Task<ActionResult<List<AuditorUserM>>> getUserbyTypeAudit()
        {
            try
            {

                return db.AuditorUserMs.Where(x => x.IsStatus != 2).ToList();
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("getRoleGroupByType/{type}")]
        public async Task<ActionResult<List<RoleGroupM>>> getRoleGroupByType(string type)
        {
            try
            {
                var res = db.RoleGroupMs.Where(x => x.UserGroup == type).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpPost("addRoleGroup")]
        public async Task<ActionResult<RoleGroupListT>> addRoleGroup(List<ManageMentMenuModel.ViewModelForSaveGroupList> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.Id == 0)
                    {
                        RoleGroupListT newGroupList = new RoleGroupListT()
                        {
                            RoleGroupMId = item.RoleGroupMId,
                            RoleMenuMId = item.RoleMenuMId,
                            IsRoleRead = item.IsRoleRead,
                            IsRoleCreate = item.IsRoleCreate,
                            IsRoleUpdate = item.IsRoleUpdate,
                            IsRoleDelete = item.IsRoleDelete,
                            IsRolePrint = item.IsRolePrint,
                            IsRoleApprove = item.IsRoleApprove,
                            IsRoleCancel = item.IsRoleCancel
                        };
                        await db.RoleGroupListTs.AddAsync(newGroupList);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        RoleGroupListT newGroupList = new RoleGroupListT();
                        newGroupList = db.RoleGroupListTs.Where(x => x.Id == item.Id).FirstOrDefault();
                        newGroupList.RoleGroupMId = item.RoleGroupMId;
                        newGroupList.RoleMenuMId = item.RoleMenuMId;
                        newGroupList.IsRoleRead = item.IsRoleRead;
                        newGroupList.IsRoleCreate = item.IsRoleCreate;
                        newGroupList.IsRoleUpdate = item.IsRoleUpdate;
                        newGroupList.IsRoleDelete = item.IsRoleDelete;
                        newGroupList.IsRolePrint = item.IsRolePrint;
                        newGroupList.IsRoleApprove = item.IsRoleApprove;
                        newGroupList.IsRoleCancel = item.IsRoleCancel;
                        db.RoleGroupListTs.Update(newGroupList);
                        db.SaveChanges();
                    }
                }

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpPost("insertRoleGrouplist")]
        public async Task<Int32> insertRoleGrouplist(RoleGroupM data)
        {
            try
            {
                await db.RoleGroupMs.AddAsync(data);
                await db.SaveChangesAsync();
                Console.WriteLine(data.Id);
                return data.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        [HttpGet("getRoleGroupById/{id}")]
        public async Task<ActionResult<RoleGroupM>> getRoleGroupById(int id)
        {
            try
            {
                var res = db.RoleGroupMs.Where(x => x.Id == id).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("getRoleGroupListByRoleGroupMId/{id}")]
        public async Task<ActionResult<List<ManageMentMenuModel.ViewModelForSaveGroupList>>> getRoleGroupListByRoleGroupMId(int id)
        {
            try
            {
                var res = db.RoleGroupListTs.Where(x => x.RoleGroupMId == id).Join(db.RoleMenuMs, p => p.RoleMenuMId, pc => pc.Id, (p, pc) => new { p, pc })
                                       .Select(x => new ManageMentMenuModel.ViewModelForSaveGroupList
                                       {
                                           RoleGroupMId = x.p.RoleGroupMId,
                                           RoleMenuMId = x.p.RoleMenuMId,
                                           IsRoleRead = x.p.IsRoleRead,
                                           IsRoleCreate = x.p.IsRoleCreate,
                                           IsRoleUpdate = x.p.IsRoleUpdate,
                                           IsRoleDelete = x.p.IsRoleDelete,
                                           IsRolePrint = x.p.IsRolePrint,
                                           IsRoleApprove = x.p.IsRoleApprove,
                                           IsRoleCancel = x.p.IsRoleCancel,
                                           AppCode = x.pc.AppCode,
                                           Id = x.p.Id,
                                           Name = x.pc.MenuName
                                       }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpPost("editRoleGrouplist")]
        public async Task<ActionResult> editRoleGrouplist(RoleGroupM data)
        {

            try
            {
                db.RoleGroupMs.Update(data);
                db.SaveChanges();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("deleteRoleGroupM/{id}")]
        public async Task<ActionResult> deleteRoleGroupM(int id)
        {

            try
            {
                var rglist = db.RoleGroupListTs.Where(x => x.RoleGroupMId == id).ToList();
                var rgm = db.RoleGroupMs.Where(x => x.Id == id).FirstOrDefault();
                db.RoleGroupMs.Remove(rgm);
                db.SaveChanges();


                foreach (var item in rglist)
                {
                    db.RoleGroupListTs.Remove(item);
                    db.SaveChanges();
                }


                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("getRoleUserMappingView/{userName}")]
        public async Task<List<ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName>> getRoleUserMappingView(string userName)
        {
            try
            {
                var res = db.RoleUserMappings.Where(x => x.UserName == userName).Join(db.RoleGroupMs, p => p.RoleGroupId, pc => pc.Id, (p, pc) => new { p, pc })
                                       .Select(x => new ManageMentMenuModel.ViewModelForGetRoleUserMappingAndName
                                       {
                                           RoleGroupId = x.p.RoleGroupId,
                                           UserName = x.p.UserName,
                                           Name = x.pc.Name
                                       }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("getRoleUserMapping/{userName}")]
        public async Task<List<RoleUserMapping>> getRoleUserMapping(string userName)
        {
            try
            {
                var res = db.RoleUserMappings.Where(x => x.UserName == userName).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("insertRoleUserMapping")]
        public async Task<ActionResult> insertRoleUserMapping(ManageMentMenuModel.ViewModelRoleGroup data)
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

        [HttpGet("deleteRoleUserMapping/{userName}")]
        public async Task<ActionResult> deleteRoleUserMapping(string userName)
        {
            try
            {
                var rglist = db.RoleUserMappings.Where(x => x.UserName == userName).ToList();
                foreach (var item in rglist)
                {
                    db.RoleUserMappings.Remove(item);
                    db.SaveChanges();
                }

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("delteRoleGroupListT/{id}")]
        public async Task<ActionResult> delteRoleGroupListT(int id)
        {

            try
            {
                var del = db.RoleGroupListTs.Where(x => x.Id == id).FirstOrDefault();

                db.RoleGroupListTs.Remove(del);
                db.SaveChanges();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("checkrolecode/{text}")]
        public async Task<ActionResult<List<RoleGroupM>>> CheckRoleCode(string text)
        {

            try
            {

                return db.RoleGroupMs.Where(x => x.RoleCode == text).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
