using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using System.Collections.Generic;
using sso.mms.helper.PortalModel;

namespace sso.mms.fees.api.Controllers.Dental.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("Dental/Ext")]
    public class CarRecordController : ControllerBase
    {
        private readonly ICarRecord extService;
        public CarRecordController(ICarRecord extService)
        {
            this.extService = extService;
        }
        [HttpGet("GetAll")]
        public async Task<List<AaiDentalCarHView>> GetAll()
        {
            try
            {
                List<AaiDentalCarHView> result = await extService.GetAll();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
         
        }

        [HttpPost("AddCarRecord")]
        public async Task<string> AddCarRecord(InsertDataViewModel carData)
        {
            var result = await extService.AddCarRecord(carData);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        
        
        [HttpPost("UpdateCarHChangeDes")]
        public async Task<string> UpdateCarHChangeDes(ChangeDesViewModel carHData)
        {
            var result = await extService.UpdateCarHChangeDes(carHData);
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
