using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using sso.mms.news.ViewModels;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly IdpDbContext db_Idp;
        private readonly UploadFileService uploadFileService;

        public NewsController(PortalDbContext portalDbContext, IdpDbContext idpDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.db_Idp = idpDbContext;
            this.uploadFileService = uploadFileService;
        }

        [HttpGet("getNewsTag")]
        public async Task<IEnumerable<NewsTagListModel>> GetNewTags(string filter)
        {
            var entities = await db.NewsTagLists.ToListAsync();
            //
            List<NewsTagListModel> newsTagListModels = new List<NewsTagListModel>();
            for (var i = 0; i < entities.Count(); i++)
            {
                if (entities[i].TagName != null)
                {
                    newsTagListModels.Add(new NewsTagListModel()
                    {
                        Id = entities[i].Id,
                        TagName = entities[i].TagName.Split(',').Where(c => c.ToLower().Contains(filter.ToLower())).Distinct().ToArray(),
                        IsActive = entities[i].IsActive,
                        IsStatus = entities[i].IsStatus,
                        NewsTId = entities[i].NewsTId,
                        UpdateBy = entities[i].UpdateBy,
                        UpdateDate = entities[i].UpdateDate,
                        CreateDate = entities[i].CreateDate,
                        CreateBy = entities[i].CreateBy,
                    });
                }


            };
            return newsTagListModels;
        }

        [HttpGet("addNewsTag")]
        public async Task<IActionResult> AddNewTags(NewsTagList newsTagList)
        {

            try
            {
                var data = new NewsTagList()
                {
                    NewsTId = newsTagList.NewsTId,
                    TagName = newsTagList.TagName,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = "",
                    UpdateDate = DateTime.Now,
                    UpdateBy = ""

                };

                await db.NewsTagLists.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }


        [HttpGet("GetDataNewsM/{newsMid}")]
        public async Task<ActionResult<IEnumerable<NewsMView>>> GetDataNewsM(int newsMid)
        {
            var result2 = await db_Idp.PositionMs.ToListAsync();
            var result3 = await db_Idp.DepartmentMs.ToListAsync();

            var result = await db_Idp.SsoUserMs
                .ToListAsync(); // Load SsoUserMs into memory

            var userViews =  result.Select(s => new SsoUserMView
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                UserName = s.UserName,
                PosName = s.SsoPersonFieldPosition,
                DepName = s.WorkingdeptDescription,
            }).ToList();

            var distinctUserViews = userViews.GroupBy(u => u.UserName)
               .Select(g => g.First())
            .ToList();

          
            var userViewsData = distinctUserViews.ToDictionary(u => u.UserName);

            var result4 = await db.NewsMs
                .GroupJoin(db.NewsTs, nm => nm.Id, nt => nt.NewsMId, (nm, nt) => new { nm, nt })
                .Select(s => new NewsMView
                {
                    Id = s.nm.Id,
                    Name = s.nm.Name,
                    IsStatus = s.nm.IsStatus,
                    ImageFileM = s.nm.ImageFileM,
                    ImagePathM = s.nm.ImagePathM,
                    IsActive = s.nm.IsActive,
                    Remark = s.nm.Remark,
                    CreateBy = s.nm.CreateBy,
                    CreateDate = s.nm.CreateDate,
                    UpdateBy = s.nm.UpdateBy,
                    UpdateDate = s.nm.UpdateDate,
                    NewsTs = s.nm.NewsTs.GroupJoin(db.NewsTagLists, nt => nt.Id, ntl => ntl.NewsTId, (nt, ntl) => new NewsTView
                    {
                        Id = nt.Id,
                        Content = nt.Content,
                        CreateBy = nt.CreateBy,
                        CreateDate = nt.CreateDate,
                        EndDate = nt.EndDate,
                        ImageFileM = nt.ImageFileM,
                        ImagePathM = nt.ImagePathM,
                        IsActive = nt.IsActive,
                        NewsMId = nt.NewsMId,
                        PinStatus = nt.PinStatus,
                        StartDate = nt.StartDate,
                        Title = nt.Title,
                        UpdateBy = nt.UpdateBy,
                        UpdateDate = nt.UpdateDate,
                        UploadFile = nt.UploadFile,
                        UploadPath = nt.UploadPath,
                        IsStatus = nt.IsStatus,
                        PrivilegePrivate = nt.PrivilegePrivate,
                        PrivilegePublic = nt.PrivilegePublic,
                        PrivilegeSso = nt.PrivilegeSso,

                        DepName = userViewsData.ContainsKey(nt.CreateBy) ? userViewsData[nt.CreateBy].DepName : null,
                        PosName = userViewsData.ContainsKey(nt.CreateBy) ? userViewsData[nt.CreateBy].PosName : null,
                        Fname = userViewsData.ContainsKey(nt.CreateBy) ? userViewsData[nt.CreateBy].FirstName : null,
                        Lname = userViewsData.ContainsKey(nt.CreateBy) ? userViewsData[nt.CreateBy].LastName : null,


                        NewsTagLists = nt.NewsTagLists.ToList(),
                    }).ToList()
                }).Where(x => x.Id == newsMid).OrderByDescending(x => x.Id).OrderByDescending(x => x.CreateDate).ToListAsync();


            return result4;


        }
        

        [HttpGet("GetDataNews/{id}")]
        public async Task<ActionResult<NewsT>> GetDataNews(int id)
        {
            var result = db.NewsTs
                  .Include(x => x.NewsTagLists)
                  .Select(s => new NewsT
                  {
                      Id = s.Id,
                      NewsMId = s.NewsMId,
                      Title = s.Title,
                      Content = s.Content,
                      ImagePathM = s.ImagePathM,
                      ImageFileM = s.ImageFileM,
                      UploadPath = s.UploadPath,
                      UploadFile = s.UploadFile,
                      StartDate = s.StartDate,
                      EndDate = s.EndDate,
                      IsActive = s.IsActive,
                      IsStatus = s.IsStatus,
                      PrivilegeSso = s.PrivilegeSso,
                      PrivilegePublic = s.PrivilegePublic,
                      PrivilegePrivate = s.PrivilegePrivate,
                      CreateDate = s.CreateDate,
                      CreateBy = s.CreateBy,
                      UpdateDate = s.UpdateDate,
                      UpdateBy = s.UpdateBy,
                      PinStatus = s.PinStatus,
                      NewsTagLists = s.NewsTagLists,
                  }).FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return await result;
            }
            else
            {
                return null!;
            }
        }

        [HttpPost("AddNews")]
        public async Task<IActionResult> AddNews(NewTransectionRequest request)
        {
            try
            {
                NewsT newTransection = new NewsT()
                {
                    Title = request.Title,
                    Content = request.Content,
                    ImagePathM = request.ImagePathM,
                    ImageFileM = request.ImageFileM, // String path + imageName
                    UploadPath = request.UploadPath,
                    UploadFile = request.UploadFile,
                    NewsMId = request.NewsMId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    PinStatus = request.PinStatus,
                    PrivilegePrivate = request.PrivilegePrivate,
                    PrivilegePublic = request.PrivilegePublic,
                    PrivilegeSso = request.PrivilegeSso,
                    IsActive = true,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = request.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = request.UpdateBy

                };
                await db.NewsTs.AddAsync(newTransection);
                await db.SaveChangesAsync();


                NotificationT notificationT = new NotificationT()
                {
                    NewTId = newTransection.Id,
                    Content = newTransection.Content,
                    Title = newTransection.Title,
                    IsActive = true,
                    NotiMId = request.NotiMId,
                    IsStatus = 1,
                    CreateDate = DateTime.Now,
                    CreateBy = request.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = request.UpdateBy

                };

                await db.NotificationTs.AddAsync(notificationT);
                await db.SaveChangesAsync();


                NewsTagList newsTagList = new NewsTagList()
                {
                    NewsTId = newTransection.Id,
                    TagName = request.TagList,
                    IsActive = request.IsActive,
                    IsStatus = request.IsStatus,
                    UpdateBy = request.UpdateBy,
                    UpdateDate = request.UpdateDate,
                    CreateBy = request.CreateBy,
                    CreateDate = request.CreateDate,
                };

                await db.NewsTagLists.AddAsync(newsTagList);
                await db.SaveChangesAsync();

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }

        }

        [HttpPost("EditNews")]
        public async Task<IActionResult> EditNews(NewTransectionRequest newsT)
        {
            try
            {
                NewsTagList? newsTagListById;

                var getNews = await db.NewsTs.FirstOrDefaultAsync(x => x.Id == newsT.Id);


                newsTagListById = await db.NewsTagLists.FirstOrDefaultAsync(x => x.NewsTId == getNews.Id);

                if (newsTagListById == null && newsT.TagList != null)
                {
                    NewsTagList newsTagList = new NewsTagList()
                    {
                        NewsTId = newsT.Id,
                        TagName = newsT.TagList,
                        IsActive = newsT.IsActive,
                        IsStatus = newsT.IsStatus,
                        UpdateBy = newsT.UpdateBy,
                        UpdateDate = newsT.UpdateDate,
                        CreateBy = newsT.CreateBy,
                        CreateDate = newsT.CreateDate,
                    };

                    await db.NewsTagLists.AddAsync(newsTagList);
                    await db.SaveChangesAsync();
                }

                if (getNews != null && newsTagListById != null)
                {
                    getNews.Title = newsT.Title;
                    getNews.Content = newsT.Content;
                    getNews.ImagePathM = newsT.ImagePathM;
                    getNews.ImageFileM = newsT.ImageFileM;
                    if (newsT.UploadPath != null)
                    {
                        getNews.UploadPath = newsT.UploadPath;
                        getNews.UploadFile = newsT.UploadFile;
                    }

                    getNews.PinStatus = newsT.PinStatus;
                    getNews.PrivilegePrivate = newsT.PrivilegePrivate;
                    getNews.PrivilegePublic = newsT.PrivilegePublic;
                    getNews.PrivilegeSso = newsT.PrivilegeSso;
                    getNews.CreateBy = newsT.CreateBy;
                    getNews.UpdateBy = newsT.UpdateBy;
                    getNews.StartDate = newsT.StartDate;
                    getNews.EndDate = newsT.EndDate;
                    getNews.UpdateDate = newsT.UpdateDate;

                    db.NewsTs.Update(getNews);
                    await db.SaveChangesAsync();

                    newsTagListById.TagName = newsT.TagList;
                    newsTagListById.IsActive = newsT.IsActive;
                    newsTagListById.IsStatus = newsT.IsStatus;
                    newsTagListById.UpdateBy = newsT.UpdateBy;
                    newsTagListById.UpdateDate = newsT.UpdateDate;
                    newsTagListById.CreateBy = newsT.CreateBy;
                    newsTagListById.CreateDate = newsT.CreateDate;

                    db.NewsTagLists.Update(newsTagListById);
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
                return Ok(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("DeleteData")]
        public async Task<IActionResult> DeleteData(NewsTView newsM)
        {
            try
            {
                var getNews = await db.NewsTs.FindAsync(newsM.Id);
                var notiT = db.NotificationTs.FirstOrDefault(item => item.NewTId == getNews.Id);
                var newsTagLists = db.NewsTagLists.Where(item => item.NewsTId == getNews.Id).ToList();
                if (getNews != null)
                {
                    if (notiT != null)
                    {
                        db.NotificationTs.Remove(notiT);
                        await db.SaveChangesAsync();
                    }
                    if (newsTagLists != null)
                    {
                        foreach (var item in newsTagLists)
                        {
                            db.NewsTagLists.Remove(item);
                            await db.SaveChangesAsync();
                        }
                    }
                    db.NewsTs.Remove(getNews);
                    await db.SaveChangesAsync();

                    return Ok(new { status = true, message = "success" });
                }
                else
                {
                    return Ok(new { status = false, message = "Edit Data Fail!" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("GetDataNewsM")]
        public async Task<ActionResult<IEnumerable<NewsM>>> GetDataNewsM()
        {
            return await db.NewsMs.ToListAsync();
        }

        [HttpGet("GetListNewsM")]
        public async Task<IEnumerable<NewsM>> GetListNewsM()
        {
            var newsToExpire = db.NewsTs.Where(x => x.EndDate <= DateTime.Now.Date).ToList();        
            foreach (var news in newsToExpire)
            {
                news.IsStatus = 0;
            }
            await db.SaveChangesAsync();
            return await db.NewsMs
                .GroupJoin(db.NewsTs, p => p.Id, pc => pc.NewsMId, (p, pc) => new { p, pc })
                .Select(x =>
                    new NewsM
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        Remark = x.p.Remark,
                        ImagePathM = x.p.ImagePathM,
                        ImageFileM = x.p.ImageFileM,
                        IsActive = x.p.IsActive,
                        IsStatus = x.p.IsStatus,
                        CreateDate = x.p.CreateDate,
                        CreateBy = x.p.CreateBy,
                        UpdateDate = x.p.UpdateDate,
                        UpdateBy = x.p.UpdateBy,
                        NewsTs = x.p.NewsTs
                        .Where(w => w.IsStatus == 1)

                         .ToList(),
                    }).ToListAsync();
        }

        [HttpPost("UploadFile")]
        public async Task<JsonResult> UploadFile([FromForm] List<IFormFile>? upload)
        {
            string Uploadurl;

            //Keep develop and production separate.
            //Uploadurl = Path Directory 
            Uploadurl = $"{ConfigureCore.redirectsNews}Files/";

            (string errorMessage, string imageName) = await uploadFileService.UploadImage(upload, "");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                return new JsonResult(new { Success = "False", responseText = errorMessage, error = new { message = "ขนาดไฟล์รูปภาพไม่เกิน 10 MB " } });
            }

            try
            {
                var success = new ResponseUpload
                {
                    //Uploaded = 1,
                    FileName = imageName,
                    Path_Url = Uploadurl + imageName,
                    Url = Uploadurl + imageName,
                };

                return new JsonResult(success);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<NewsM>>> SearchNews(SearchModel value)
        {
            return await db.NewsMs.Where(x => x.Id == value.newsMid).GroupJoin(db.NewsTs, nm => nm.Id, nt => nt.NewsMId, (nm, nt) => new { nm, nt })
              .Select(s => new NewsM
              {
                  Id = s.nm.Id,
                  Name = s.nm.Name,
                  IsStatus = s.nm.IsStatus,
                  ImageFileM = s.nm.ImageFileM,
                  ImagePathM = s.nm.ImagePathM,
                  IsActive = s.nm.IsActive,
                  Remark = s.nm.Remark,
                  CreateBy = s.nm.CreateBy,
                  CreateDate = s.nm.CreateDate,
                  UpdateBy = s.nm.UpdateBy,
                  UpdateDate = s.nm.UpdateDate,
                  NewsTs = s.nm.NewsTs.GroupJoin(db.NewsTagLists, nt => nt.Id, ntl => ntl.NewsTId, (nt, ntl) => new NewsT
                  {
                      Id = nt.Id,
                      Content = nt.Content,
                      CreateBy = nt.CreateBy,
                      CreateDate = nt.CreateDate,
                      EndDate = nt.EndDate,
                      ImageFileM = nt.ImageFileM,
                      ImagePathM = nt.ImagePathM,
                      IsActive = nt.IsActive,
                      NewsMId = nt.NewsMId,
                      PinStatus = nt.PinStatus,
                      StartDate = nt.StartDate,
                      Title = nt.Title,
                      UpdateBy = nt.UpdateBy,
                      UpdateDate = nt.UpdateDate,
                      UploadFile = nt.UploadFile,
                      UploadPath = nt.UploadPath,
                      IsStatus = nt.IsStatus,
                      PrivilegePrivate = nt.PrivilegePrivate,
                      PrivilegePublic = nt.PrivilegePublic,
                      PrivilegeSso = nt.PrivilegeSso,
                      NewsTagLists = nt.NewsTagLists.ToList(),
                  }).Where(w => w.Title.Contains(value.searchTerm)
                  || w.NewsTagLists.Any(tag => tag.NewsTId == w.Id && tag.TagName.Contains(value.searchTerm))).ToList()
              })
              .OrderByDescending(x => x.CreateDate).ToListAsync();
            //var searchTitleAndCreateDate = await db.NewsMs
            //      .Where(w => w.Id == value.newsMid)
            //      .GroupJoin(db.NewsTs, p => p.Id, pc => pc.NewsMId, (p, pc) => new { p, pc })
            //      .Select(x =>
            //          new NewsM
            //          {
            //              Id = x.p.Id,
            //              Name = x.p.Name,
            //              Remark = x.p.Remark,
            //              ImagePathM = x.p.ImagePathM,
            //              ImageFileM = x.p.ImageFileM,
            //              IsActive = x.p.IsActive,
            //              IsStatus = x.p.IsStatus,
            //              CreateDate = x.p.CreateDate,
            //              CreateBy = x.p.CreateBy,
            //              UpdateDate = x.p.UpdateDate,
            //              UpdateBy = x.p.UpdateBy,
            //              NewsTs = x.p.NewsTs.Where(w =>
            //               w.Title.Contains(value.searchTerm)
            //              || w.CreateDate.ToString().Contains(value.searchTerm)
            //              || w.UpdateDate.ToString().Contains(value.searchTerm)
            //              )
            //              .OrderByDescending(o => o.CreateDate).ToList()
            //          }).ToListAsync();


            //var searchTagsList = await db.NewsMs
            //      .Where(w => w.Id == value.newsMid)
            //      .GroupJoin(db.NewsTs, p => p.Id, pc => pc.NewsMId, (p, pc) => new { p, pc })
            //      .Select(x =>
            //          new NewsM
            //          {
            //              Id = x.p.Id,
            //              Name = x.p.Name,
            //              Remark = x.p.Remark,
            //              ImagePathM = x.p.ImagePathM,
            //              ImageFileM = x.p.ImageFileM,
            //              IsActive = x.p.IsActive,
            //              IsStatus = x.p.IsStatus,
            //              CreateDate = x.p.CreateDate,
            //              CreateBy = x.p.CreateBy,
            //              UpdateDate = x.p.UpdateDate,
            //              UpdateBy = x.p.UpdateBy,
            //              NewsTs = x.p.NewsTs.OrderByDescending(o => o.CreateDate).ToList()
            //          }).ToListAsync();


            //if (searchTitleAndCreateDate[0].NewsTs.Count() != 0)
            //{
            //    foreach (var itemNewsT in searchTitleAndCreateDate[0].NewsTs)
            //    {
            //        if (itemNewsT != null)
            //        {
            //            var queryTagsList = await db.NewsTagLists.Where(x => x.NewsTId == itemNewsT.Id && x.TagName.Contains(value.searchTerm)).ToListAsync();
            //            var tags = await db.NewsTagLists.Where(x => x.TagName.Contains(value.searchTerm)).ToListAsync();
            //            if (queryTagsList.Count() == 0 && tags.Count() == 0)
            //            {
            //                var data = await db.NewsTagLists.Where(x => x.NewsTId == itemNewsT.Id).ToListAsync();
            //            }
            //            else
            //            {
            //                searchTagsList[0].NewsTs = searchTagsList[0].NewsTs.Take(tags.Count()).Where(x=>x.NewsTagLists.Count() != 0).ToList();
            //                return searchTagsList;
            //            }
            //        }
            //        else
            //        {
            //            var queryTagsList = await db.NewsTagLists.Where(x => x.NewsTId == itemNewsT.Id).ToListAsync();
            //        }
            //    }
            //    return searchTitleAndCreateDate;
            //}
            //else
            //{
            //    var result = await db.NewsTagLists.Where(x=> x.TagName.Contains(value.searchTerm)).ToListAsync();
            //    searchTagsList[0].NewsTs = searchTagsList[0].NewsTs.Take(result.Count()).ToList();

            //    return searchTagsList;
            //}

        }
    }
}
