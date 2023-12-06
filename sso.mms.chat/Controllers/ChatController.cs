using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.chat.Pages;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.NetworkInformation;
using System.Linq;
using sso.mms.helper.Configs;
using sso.mms.chat.ViewModels;
using System.Net.Http;

namespace sso.mms.chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly UploadFileService uploadFileService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory, PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.uploadFileService = uploadFileService;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet("GetChat")]
        public async Task<ActionResult<IEnumerable<ChatT>>> GetChat()
        {
            return await db.ChatTs.Where(c => c.IsActive == true).ToListAsync();
        }
        [HttpGet("GetChatFromLog")]
        public async Task<ActionResult<IEnumerable<ChatT>>> GetChatFromLog()
        {
            return await db.ChatTs
                    .Where(c => c.IsActive == true)
                    .GroupJoin(db.ChatLogs, ct => ct.Id, cl => cl.ChatTId, (ct, cl) => new { ct, cl })
                    .Select(s =>
                    new ChatT
                    {
                        Id = s.ct.Id,
                        ChatRoomId = s.ct.ChatRoomId,
                        ChatLogs = s.ct.ChatLogs.Where(w => w.IsStatus == 1).ToList()
                    })
                    .ToListAsync();
        }

        [HttpPost("UpdateChatReadStatus")]
        public async Task<ActionResult> UpdateChatReadStatus(List<ChatLog> chat)
        {
            try
            {
                foreach (var item in chat)
                {
                    var getChatId = await db.ChatLogs.FindAsync(item.Id);
                    if (getChatId != null)
                    {
                        getChatId.UpdateDate = DateTime.Now;
                        getChatId.IsStatus = 0;
                        db.ChatLogs.Update(getChatId);
                        await db.SaveChangesAsync();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { massage = ex.Message });
            }

        }


        [HttpGet("byRoomId/{roomId}")]
        public async Task<ActionResult<IEnumerable<ChatT>>> ByRoomId(int roomId)
        {
            return await db.ChatTs.Where(c => c.ChatRoomId == roomId).OrderByDescending(o => o.CreateDate).Take(10).OrderBy(o => o.Id).ToListAsync();
        }

        [HttpGet("getChatHistory")]
        public async Task<ActionResult<IEnumerable<ChatT>>> GetChatHistory(int roomId, int pageNumber, int pageSize)
        {
            var pagedData = await db.ChatTs.Where(w => w.ChatRoomId == roomId)
               .Skip((pageNumber - 1) * pageSize).OrderByDescending(o => o.CreateDate).Take(pageSize).OrderBy(o => o.Id).ToListAsync();

            return pagedData;
        }

        [HttpPost("ChatMessage")]
        public async Task<IActionResult> ChatMessage(ChatT message)
        {

            try
            {
                message.IsActive = true;
                message.IsStatus = 1;
                message.CreateDate = DateTime.Now;
                message.UpdateDate = DateTime.Now;

                await db.ChatTs.AddAsync(message);
                await db.SaveChangesAsync();
                ChatLog chatlog = new ChatLog()
                {
                    ChatTId = message.Id,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    UserType = message.UserType,
                    CreateBy = message.SenderName,
                    UpdateDate = DateTime.Now,
                    UpdateBy = message.SenderName
                };
                if (message.UserType == 2)
                {
                    chatlog.IsStatus = 1;
                }
                else if (message.UserType == 1)
                {
                    chatlog.IsStatus = 2;
                }
                await db.ChatLogs.AddAsync(chatlog);
                await db.SaveChangesAsync();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }

        }

        [HttpPost("[action]")]
        public async Task<ResponseUpload> UploadFile([FromForm] List<IFormFile>? upload)
        {
            string Uploadurl;

            Uploadurl = $"{ConfigureCore.redirectChat}Files/";

            (string errorMessage, string UploadImagePath) = await uploadFileService.UploadImage(upload, "");
            Console.WriteLine(errorMessage);
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
        [HttpGet("GetChatNotiFromLog")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatNotiFromLog(int hosMId)
        {
            return await db.ChatRooms.Where(w => w.HospitalMId == hosMId)
                .Include(ct => ct.ChatTs.Where(c => c.IsActive == true && c.UserType == 1))
                .ThenInclude(cl => cl.ChatLogs.Where(w=>w.IsStatus == 2))
                .ToListAsync();
        }

        [HttpPost("UpdateChatReadStatusNoti")]
        public async Task<ActionResult> UpdateChatReadStatusNoti(List<ChatLog> chat)
        {
            try
            {
                foreach (var item in chat)
                {
                    var getChatId = await db.ChatLogs.FindAsync(item.Id);
                    if (getChatId != null)
                    {
                        getChatId.UpdateDate = DateTime.Now;
                        getChatId.IsStatus = 3;
                        db.ChatLogs.Update(getChatId);
                        await db.SaveChangesAsync();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { massage = ex.Message });
            }
        }
    }
}
