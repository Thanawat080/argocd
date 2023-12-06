using AntDesign;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.chat.ViewModels;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.login.Interface;
using sso.mms.login.Services;
using System.Linq;
using System.Net.NetworkInformation;

namespace sso.mms.chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly PortalDbContext db;
        private readonly IUserRoleService userRoleService;
        private readonly IHttpClientFactory _httpClientFactory;
        public ChatRoomController(IHttpClientFactory httpClientFactory, PortalDbContext portalDbContext, HttpClient httpClient)
        {
            this.db = portalDbContext;
            this.userRoleService = userRoleService;
            _httpClientFactory = httpClientFactory;

        }

        [HttpPost("CreateChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> CreateChatRoom(ChatRoom chatRoom)
        {

            try
            {
                var createChatRoom = new ChatRoom()
                {
                    ChatRoomMId = chatRoom.ChatRoomMId,
                    HospitalMId = chatRoom.HospitalMId,
                    Name = chatRoom.Name,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = chatRoom.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = chatRoom.UpdateBy
                };

                var result = await db.ChatRooms.AddAsync(createChatRoom);
                await db.SaveChangesAsync();

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("getChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom([FromQuery] QueryStringModel queryStringModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);

                var response = await httpClient.GetFromJsonAsync<UserRole>("api/userrole/getuserrole/" + queryStringModel.username);
                
                ChatRoomM findChatRoomMId = new ChatRoomM();
                if (response != null)
                {
                    var checkChatRoomByMenuRole =
                        await db.ChatRooms
                        .GroupJoin(db.ChatRoomMs,
                        cr => cr.ChatRoomMId,
                        crm => crm.Id,
                        (cr, crm) => new { cr, crm })
                        .Join(db.HospitalMs,
                        hs => hs.cr.HospitalMId,
                        c => c.Id, (hs, c) => new
                        { hs.cr, hs.crm, c })
                        
                   .Select(s => new ChatRoom
                   {
                       Id = s.cr.Id,
                       ChatRoomMId = s.cr.ChatRoomMId,
                       HospitalMId = s.cr.HospitalMId,
                       Name = s.cr.Name,
                       IsActive = s.cr.IsActive,
                       IsStatus = s.cr.IsStatus,
                       CreateDate = DateTime.Now,
                       CreateBy = s.cr.CreateBy,
                       UpdateDate = DateTime.Now,
                       ChatRoomM = s.cr.ChatRoomM,
                       HospitalM = s.cr.HospitalM

                   }).Where(w => w.HospitalMId == queryStringModel.hospitalId).ToListAsync();

                    foreach (var itemRole in response.role)
                    {
                        foreach (var item in itemRole.menu) {
                            //เมื่อเข้ามาให้ทำการเช็คค่าบริการว่าเขามี role menu เกี่ยวกับค่าบริการหรือไม่
                            var chatRoomMListByCodeHos = checkChatRoomByMenuRole.Where(x => x.ChatRoomM.CodeHos == item.menuCode).FirstOrDefault();
                            var getChatRoomMId = db.ChatRoomMs.Where(x => x.CodeHos == item.menuCode).FirstOrDefault();

                            //หากมี role menu เกี่ยวกับค่าบริการนั้นให้ ตรวจสอบดูก่อนว่าเขามี ห้องแชทหรือยัง
                            if (chatRoomMListByCodeHos == null && getChatRoomMId != null)
                            {
                                //หากไม่มี ห้องแชท ให้ทำการสร้างห้องแชทนั้น
                                var save = new ChatRoom();
                                save.ChatRoomMId = getChatRoomMId.Id;
                                save.HospitalMId = queryStringModel.hospitalId;
                                save.Name = Guid.NewGuid().ToString();
                                save.CreateBy = queryStringModel.username;
                                save.UpdateBy = queryStringModel.username;

                                var findByHospitalMId = await db.HospitalMs.FirstOrDefaultAsync(w => w.Id == queryStringModel.hospitalId);

                                if (findByHospitalMId != null)
                                {
                                    var data = await CreateChatRoom(save);
                                }
                            }
                        }
                    }

                    var getChatRoomByHospitalId = 
                        await db.ChatRooms
                        .GroupJoin(db.ChatRoomMs, 
                        cr => cr.ChatRoomMId, 
                        crm => crm.Id, (cr, crm) =>
                        new { cr, crm })
                       .Join(db.HospitalMs, 
                       hs => hs.cr.HospitalMId, 
                       c => c.Id, (hs, c) => new { hs.cr, hs.crm, c })
                       .Select(s => new ChatRoom
                       {
                           Id = s.cr.Id,
                           ChatRoomMId = s.cr.ChatRoomMId,
                           HospitalMId = s.cr.HospitalMId,
                           Name = s.cr.Name,
                           IsActive = s.cr.IsActive,
                           IsStatus = s.cr.IsStatus,
                           CreateDate = DateTime.Now,
                           CreateBy = s.cr.CreateBy,
                           UpdateDate = DateTime.Now,
                           ChatRoomM = s.cr.ChatRoomM,
                           HospitalM = s.cr.HospitalM,
                           ChatTs = db.ChatTs.Where(w => w.ChatRoomId == s.cr.Id).ToList()

                       }).Where(w => w.HospitalMId == queryStringModel.hospitalId).ToListAsync();


                    List<ChatRoom> chatRoomList = new List<ChatRoom>();
                    foreach (var itemRole in response.role)
                    {
                        foreach (var item in itemRole.menu)
                        {
                            var data = getChatRoomByHospitalId.Where(x => x.ChatRoomM.CodeHos == item.menuCode).FirstOrDefault();
                            if (data != null)
                            {
                                chatRoomList.Add(data);
                            }
                        }
                    }

                   var resultDistinctList = chatRoomList.DistinctBy(x => x.Id).ToList();

                    return resultDistinctList.ToList();
                }
                else
                {
                    return BadRequest("you don't have a data");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpGet("getChatRoomMaster")]
        public async Task<ActionResult<IEnumerable<ChatRoomM>>> GetChatRoomMaster([FromQuery] QueryStringChatGroupModel queryString)
        {

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(ConfigureCore.baseAddressLogin);

            var response = await httpClient.GetFromJsonAsync<UserRole>("api/userrole/getuserrole/" + queryString.username);

            var getChatRoomM = await db.ChatRoomMs.GroupJoin(db.ChatRooms, crm => crm.Id, cr => cr.ChatRoomMId, (cr, crm) => new { cr, crm })
               .Select(s => new ChatRoomM
               {
                   Id = s.cr.Id,
                   Name = s.cr.Name,
                   CodeHos = s.cr.CodeHos,
                   CodeSso = s.cr.CodeSso,
                   ChatRooms = s.cr.ChatRooms.Join(db.HospitalMs, cr => cr.HospitalMId, hs => hs.Id, (cr, hs) => new ChatRoom
                   {
                       Id = cr.Id,
                       ChatRoomMId = cr.ChatRoomMId,
                       Name = cr.Name,
                       IsActive = cr.IsActive,
                       IsStatus = cr.IsStatus,
                       CreateBy = cr.CreateBy,
                       CreateDate = cr.CreateDate,
                       UpdateBy = cr.UpdateBy,
                       UpdateDate = cr.UpdateDate,
                       HospitalMId = cr.HospitalMId,
                       HospitalM = hs,
                       ChatTs = db.ChatTs.Where(w => w.ChatRoomId == cr.Id).ToList()
                       
                   }).ToList()

               }).ToListAsync();

            List<ChatRoomM> chatRoomMList = new List<ChatRoomM>();


            foreach (var itemRole in response.role)
            {
                foreach (var item in itemRole.menu) 
                {
                    var data = getChatRoomM.Where(x => x.CodeSso == item.menuCode).FirstOrDefault();
                    if (data != null)
                    {
                        chatRoomMList.Add(data);
                    }
                }
            }

            return chatRoomMList;

        }

        [HttpGet("GetChatByRole")]
        public async Task<ActionResult<IEnumerable<ChatT>>> GetChatByRole()
        {
            return await db.ChatTs.Where(c => c.IsActive == true).ToListAsync();
        }
    }
}
