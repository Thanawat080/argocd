using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.Dental.Base;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using System.Collections.Generic;
using sso.mms.helper.PortalModel;

namespace sso.mms.fees.api.Controllers.Dental.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("Dental/Base")]
    public class ProvinceController : ControllerBase
    {
        private readonly IMProvince BaseService;
        public ProvinceController(IMProvince extService)
        {
            this.BaseService = extService;
        }
        [HttpGet("GetProvince")]
        public async Task<List<AdbMProvince>> GetProvince()
        {   
           List<AdbMProvince> result = await BaseService.GetProvince();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        [HttpGet("GetDistrict")]
        public async Task<List<AdbMDistrict>> GetDistrict(int provinceId)
        {   
           List<AdbMDistrict> result = await BaseService.GetDistrict(provinceId);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        [HttpGet("GetSubDistrict")]
        public async Task<List<AdbMSubdistrict>> GetSubDistrict(int disId)
        {   
           List<AdbMSubdistrict> result = await BaseService.GetSubDistrict(disId);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        
    }
}
