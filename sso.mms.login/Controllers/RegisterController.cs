using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IdpDbContext db;
        private readonly PortalDbContext portalDb;
        private readonly HttpClient httpClient;

        public RegisterController(IdpDbContext idpDbContext,PortalDbContext portalDb, HttpClient httpClient)
        {
            this.db = idpDbContext;
            this.portalDb = portalDb;
            this.httpClient = httpClient;
        }

        //[HttpPost("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register(HospitalUserM hospitalUserM)
        {
            try
            {
                await db.HospitalUserMs.AddAsync(hospitalUserM);
                await db.SaveChangesAsync();

                // sync hos
                var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncHosUser");
                Console.WriteLine(responseadduser);

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }



        [HttpPost("CheckDopa")]
        public async Task<ResponseModel> CheckDopa(CheckDopaModel dopaData)
        {
            var response = new ResponseModel();
            try
            {
                response = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "data is by pass"
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message
                };
                return response;
            }

        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<HospitalM>>> checkHospCode()
        {
            var response = new ResponseModel();
            try
            {
              List<HospitalM> result = await portalDb.HospitalMs.Where(f => f.IsActive == true).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> addHospital(HospitalM hospital)
        {
            var response = new ResponseModel();
            try
            {
               await portalDb.HospitalMs.AddAsync(hospital);
               await portalDb.SaveChangesAsync();
                response = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "Insert Success !"
                };
                return response;
            }
            catch(Exception ex)
            {
                response = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = "Insert fail !" + ex.Message
                };
                return response;
            }
        }
    }
}
