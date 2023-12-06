using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Data;
using sso.mms.login.ViewModels.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IdpDbContext db;
        public MasterController(IdpDbContext idpDbContext)
        {
            this.db = idpDbContext;
        }

        [HttpGet("getProvince")]
        public async Task<ActionResult<IEnumerable<ProvinceM>>> GetProvince()
        {
            return await db.ProvinceMs.ToArrayAsync();
        }

        [HttpGet("getDistrict/{ProvinceCode}")]
        public async Task<ActionResult<IEnumerable<DistrictM>>> GetDistrict(string ProvinceCode)
        {
            return await db.DistrictMs.Where(x=>x.ProvinceCode == ProvinceCode).ToListAsync();
        }

        [HttpGet("getSubDistrict/{DistrictCode}")]
        public async Task<ActionResult<IEnumerable<SubdistrictM>>> GetSubDistrict(string DistrictCode)
        {
            return await db.SubdistrictMs.Where(x => x.DistrictCode == DistrictCode).ToListAsync();
        }
    }
}
