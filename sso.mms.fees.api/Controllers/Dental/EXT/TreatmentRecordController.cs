using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Controllers.Dental.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("Dental/Ext")]
    public class TreatmentRecordcontroller
    {
        private readonly ITreatmentRecord extServices;
        public TreatmentRecordcontroller(ITreatmentRecord services)
        {
            extServices = services;
        }
        [HttpGet("TreatmentRecordList")]
        public async Task<ResponseList<AaiDentalCheckHView>> TreatmentRecordList()
        {
            try
            {
                var result = await extServices.TreatmentRecordList();
                if (result != null)
                {
                    return new ResponseList<AaiDentalCheckHView>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "success"

                    };
                }
               
                    return new ResponseList<AaiDentalCheckHView>
                    {
                        ResultList = new List<AaiDentalCheckHView>(),
                        Status = 400,
                        Message = "NotFound"

                    };
                
            }
            catch (Exception ex)
            {
                return new ResponseList<AaiDentalCheckHView>
                {
                    ResultList = new List<AaiDentalCheckHView>(),
                    Status = 500,
                    Message = ex.Message

                };
            }
        }
        [HttpGet("TreatmentRecordById/{id}")]
        public async Task<Response<AaiDentalCheckHView>> TreatmentRecordById (int id)
        {
            try {
                var result = await extServices.TreatmentRecordById(id);
                if(result != null)
                {
                    return new Response<AaiDentalCheckHView>
                    {
                        Result = result,
                        Status = 200,
                        Message = "success"
                    };
                }
                return new Response<AaiDentalCheckHView>
                {
                    Result = null,
                    Status = 400,
                    Message = "NotFound"

                };

            }
            catch (Exception ex)
            {
                return new Response<AaiDentalCheckHView>
                {
                    Result = null,
                    Status = 500,
                    Message = ex.Message

                };
            }
        }
        [HttpGet("Api850/{personid}")]
        public async Task<apiview850> api850(string personid)
        {
            try { 
                return await extServices.api850(personid);
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpPost("TreatmentCreate")]
        public async Task<string> TreatmentCreate(AaiDentalCheckHView data)
        {
            try
            {
            var result = await extServices.TreatmentCreate(data);
                return "sucess";
            }catch (Exception ex)
            {
                return ex.Message;
            }

        
        }
        [HttpPost("DentalCheckupCreate")]
        public async Task<string> DentalCheckupCreate(AaiDentalCheckDView data)
        {
            try
            {
                var result = await extServices.DentalCheckupCreate(data);
                    return "success";

            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("ToothList")]
        public async Task<ResponseList<AaiDentalToothTypeM>> ToothList()
        {
            try
            {
                var result = await extServices.ToothList();
                if (result != null)
                {
                    return new ResponseList<AaiDentalToothTypeM>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "success"
                    };
                }
                return new ResponseList<AaiDentalToothTypeM>
                {
                    ResultList = null,
                    Status = 400,
                    Message = "NotFound"

                };

            }
            catch (Exception ex)
            {
                return new ResponseList<AaiDentalToothTypeM>
                {
                    ResultList = null,
                    Status = 500,
                    Message = ex.Message

                };
            }
        }
        [HttpGet("CheckDList/{id}")]
        public async Task<ResponseList<AaiDentalCheckDView>> CheckDList(int id)
        {
            try
            {
                var result = await extServices.CheckDList(id);
                if (result != null)
                {
                    return new ResponseList<AaiDentalCheckDView>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "success"
                    };
                }
                return new ResponseList<AaiDentalCheckDView>
                {
                    ResultList = null,
                    Status = 400,
                    Message = "NotFound"

                };

            }
            catch (Exception ex)
            {
                return new ResponseList<AaiDentalCheckDView>
                {
                    ResultList = null,
                    Status = 500,
                    Message = ex.Message

                };
            }
        }

    }
}
