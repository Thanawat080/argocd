using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
using sso.mms.notification.ViewModel;
using System.Linq;
using sso.mms.notification.ViewModel.BoardCast;
using sso.mms.notification.ViewModel.Response;

using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using AutoMapper;
using System.Text.RegularExpressions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly PortalDbContext db;

        public NotificationController(PortalDbContext portalDbContext)
        {
            this.db = portalDbContext;
        }

        [HttpGet("GetListCategoryM")]
        public async Task<IEnumerable<NotificationM>> GetListCategoryM()
        {
            return await db.NotificationMs
                .GroupJoin(db.NotificationTs, p => p.Id, pc => pc.NotiMId, (p, pc) => new { p, pc })
                .Select(x =>
                    new NotificationM
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        Remark = x.p.Remark,
                        IsActive = x.p.IsActive,
                        IsStatus = x.p.IsStatus,
                        CreateDate = x.p.CreateDate,
                        CreateBy = x.p.CreateBy,
                        UpdateDate = x.p.UpdateDate,
                        UpdateBy = x.p.UpdateBy,
                        NotificationTs = x.p.NotificationTs.Where(w => w.IsStatus == 1).ToList(),
                    }).ToListAsync();

        }

        [HttpPost("GetNotiTByUser")]
        public async Task<IEnumerable<NotiT>> GetNotiTByUser(NotiTApiModel notiT)
        {
            Console.WriteLine("step1", notiT.orgCode,notiT.roleCodeList,notiT.username,notiT.userType);
            var notiApi = await db.NotificationTs.Where(x => x.NotiMId == null).ToListAsync();
            Console.WriteLine("step2", notiApi);
            var notidefault = await db.NotificationTs.Where(x => x.NotiMId != null).ToListAsync();
            Console.WriteLine("step3", notidefault);
            var notiLog = await db.NotificationLogs.Where(item => item.CreateBy == notiT.username).ToListAsync();
            var notiM = await db.NotificationMs.ToListAsync();


            Console.WriteLine("step4", notiApi);

            if (notiApi != null)
            {
                // filter userType
                if (notiT.userType == "sso-mms-admin")
                {
                    notiApi = notiApi.Where(tsl => tsl.UserType == "C" || tsl.UserType == "P").ToList();
                }
                if (notiT.userType == "sso-mms-hospital")
                {
                    notiApi = notiApi.Where(tsl => tsl.UserType == "H").ToList();
                }

                // filter by Role => RoleCode
                var tranSactionByRole = notiApi.Where(x => x.NotifyOption == "R" && notiT.roleCodeList.Contains(x.RoleCode) && x.OrgCode == notiT.orgCode).ToList();


                // filter by UserName => userName
                var tranSactionByUser = notiApi.Where(x => x.NotifyOption == "U" && x.UserName == notiT.username).ToList();


                notiApi = tranSactionByRole.Concat(tranSactionByUser).OrderByDescending(tsl => tsl.CreateDate).ToList();

                notiApi = notiApi.Join(notiM, t2 => t2.AppCode, t1 => t1.AppCode, (t2, t1) => new NotificationT
                {
                    NotiMId = t1.Id,
                    Id = t2.Id,
                    Title = t2.Title,
                    Content = t2.Content,
                    IsActive = t2.IsActive,
                    IsStatus = t2.IsStatus,
                    CreateBy = t2.CreateBy,
                    CreateDate = t2.CreateDate,
                    UpdateBy = t2.UpdateBy,
                    UpdateDate = t2.UpdateDate,
                    UserType = t2.UserType,
                    NotifyOption = t2.NotifyOption,
                    OrgCode = t2.OrgCode,
                    RoleCode = t2.RoleCode,
                    UserName = t2.UserName,
                    AppCode = t2.AppCode,
                    Url = t2.Url,
                    UrlText = t2.UrlText,
                    IdRef = t2.IdRef,
                    NewTId = t2.NewTId,
                    NewT = t2.NewT,
                    NotificationLogs = t2.NotificationLogs
                })
                .ToList();
            }

            Console.WriteLine("notidefault", notidefault);
            var res = notidefault.Concat(notiApi).OrderByDescending(tsl => tsl.CreateDate).ToList();

            Console.WriteLine("step5", res);
            // Auto Mapping NotificationT => NotiT
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NotificationT, NotiT>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<List<NotiT>>(res);


            Console.WriteLine("step6", result);
            //MAPPING NOTI LOG WITH NOTITLIST IF NOTIT HAS LOG WITH USERNAME = READ
            result = result.GroupJoin(notiLog, t1 => t1.Id, t2 => t2.NotiTId, (t1, t2) => new NotiT
            {
                Id = t1.Id,
                NotiMId = t1.NotiMId,
                Title = t1.Title,
                Content = t1.Content,
                IsActive = t1.IsActive,
                IsStatus = t2.Any() ? 0 : 1, // IF notiT not have notiLog with this username Isstatus = 1 else = 0
                CreateDate = t1.CreateDate,
                CreateBy = t1.CreateBy,
                UpdateBy = t1.UpdateBy,
                UpdateDate = t1.UpdateDate,
                DivClass = t1.DivClass,
                AppCode = t1.AppCode}).OrderByDescending(item => item.IsStatus).ThenByDescending(item => item.CreateDate).ToList();

            return result;

        }

        [HttpPost("GetNotiMByUser")]
        public async Task<IEnumerable<NotificationM>> GetNotiMByUser(List<NotiT> notiT)
        {
            var notiM = await db.NotificationMs.ToListAsync();
            // Auto Mapping NotificationT => NotiT
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NotiT, NotificationT>());
            var mapper = config.CreateMapper();
            var notiTs = mapper.Map<List<NotificationT>>(notiT);

            var res = notiM.GroupJoin(notiTs, p => p.Id, pc => pc.NotiMId, (p, pc) => new NotificationM
            {
                Id = p.Id,
                Name = p.Name,
                Remark = p.Remark,
                IsActive = p.IsActive,
                IsStatus = p.IsStatus,
                CreateDate = p.CreateDate,
                CreateBy = p.CreateBy,
                UpdateBy = p.UpdateBy,
                UpdateDate = p.UpdateDate,
                NotiCode = p.NotiCode,
                AppCode = p.AppCode,
                Sequence = p.Sequence,
                NotificationTs = pc.Where(item => item.IsStatus == 1).ToList()
            }).OrderBy(nt => nt.Id).ToList();

            return res;

        }

        [HttpGet("getNotiNewsT")]
        public async Task<IEnumerable<NotificationM>> GetNotiNewsT()
        {
            //.Include()
            var query = await db.NotificationMs
                .Select(s => new NotificationM
                {
                    Id = s.Id,
                    Remark = s.Remark,
                    IsActive = s.IsActive,
                    IsStatus = s.IsStatus,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    UpdateDate = s.UpdateDate,
                    UpdateBy = s.UpdateBy,
                    //NotificationTs = s.NotificationTs.Join(db.NewsTs, x=>x.),
                    //NotificationTs = s.NotificationTs.Where(w => w.IsStatus == 1 && w.IsActive == true).ToList(),
                }).Where(w => w.IsStatus == 1 && w.IsActive == true).ToListAsync();

            return query;
        }

        [HttpGet("getNotiT")]
        public async Task<IEnumerable<NotificationTModel>> GetListNotiTransection()
        {
            return await db.NotificationTs.Select(e => new NotificationTModel
            {
                Id = e.Id,
                Title = e.Title,
                Content = e.Content,
                CreateDate = e.CreateDate,
                NotiMid = e.NotiMId
            }).OrderByDescending(e => e.Id).ToListAsync();

        }

        [HttpPost("addNotificationT")]
        public async Task<ActionResult<NotificationT>> AddNotificationT(NotificationT notificationTModel)
        {

            if (notificationTModel != null)
            {
                await db.NotificationTs.AddAsync(notificationTModel);
                await db.SaveChangesAsync();
                NotificationLog Notilog = new NotificationLog()
                {
                    NotiTId = notificationTModel.Id,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = "",
                    UpdateDate = DateTime.Now,
                    UpdateBy = ""
                };
                await db.NotificationLogs.AddAsync(Notilog);
                await db.SaveChangesAsync();

                return Ok(new ResponseStatus { Message = "Insert record success", Status = true });
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("UpdateNotiT/{notiTId}/{Status}")]
        public async Task<int?> UpdateStatusNotiT(int notiTId, int Status)
        {
            var result = await db.NotificationTs.FirstOrDefaultAsync(x => x.Id == notiTId);
            var data = new NotificationLog
            {
                NotiTId = notiTId,
                IsActive = true,
                IsStatus = 0,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            await db.NotificationLogs.AddAsync(data);
            result.IsStatus = Status;
            await db.SaveChangesAsync();

            return result.NotiMId;

        }

        [HttpGet("GetNotiTbyId/{notiMId}")]
        public async Task<ActionResult<IEnumerable<NotificationT>>> GetNotiTbyId(int notiMId)
        {
            var query = await db.NotificationTs
                .Where(x => x.NotiMId == notiMId)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();

            return query;
        }

        [HttpGet("GetNotiTDatabyId/{notiT}")]
        public async Task<ActionResult<NotificationT>> GetNotiTDatabyId(int notiT)
        {
            return await db.NotificationTs.FirstOrDefaultAsync(x => x.Id == notiT);
        }


        [HttpPost("add")]
        public async Task<ActionResult<NotificationPModel>> AddNotification(NotificationPModel notificationPModel)
        {

            if (notificationPModel != null)
            {
                NotificationT notificationT = new NotificationT()
                {
                    UserType = notificationPModel.user_type,
                    NotifyOption = notificationPModel.notify_option,
                    OrgCode = notificationPModel.org_code,
                    RoleCode = notificationPModel.role_code,
                    UserName = notificationPModel.user_name,
                    AppCode = notificationPModel.app_code,
                    Title = notificationPModel.title,
                    Content = notificationPModel.content,
                    Url = notificationPModel.url,
                    IdRef = notificationPModel.id_ref,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = "",
                    UpdateDate = DateTime.Now,
                    UpdateBy = ""
                };
                await db.NotificationTs.AddAsync(notificationT);
                await db.SaveChangesAsync();
                NotificationLog Notilog = new NotificationLog()
                {
                    NotiTId = notificationT.Id,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = "",
                    UpdateDate = DateTime.Now,
                    UpdateBy = ""
                };
                await db.NotificationLogs.AddAsync(Notilog);
                await db.SaveChangesAsync();

                return Ok(new ResponseStatus { Message = "Insert record success", Status = true });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("getAppNotiT")]
        public async Task<ActionResult<IEnumerable<NotificationT>>> getAppNotiT()
        {
            return await db.NotificationTs.Where(x => x.NotiMId == null)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        }

        [HttpGet("[action]/{username}")]
        public async Task<ActionResult<IEnumerable<NotificationLog>>> GetNotiLogByuser(string username)
        {
            return await db.NotificationLogs.Where(item => item.CreateBy == username).ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<NotificationPModel>> AddNotificationLog(NotificationLog Notilog)
        {
            if (Notilog != null)
            {
                Notilog.IsActive = true;
                Notilog.IsStatus = 1;
                Notilog.CreateDate = DateTime.Now;
                Notilog.UpdateDate = DateTime.Now;
                await db.NotificationLogs.AddAsync(Notilog);
                await db.SaveChangesAsync();

                return Ok(new ResponseStatus { Message = "Insert record success", Status = true });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
