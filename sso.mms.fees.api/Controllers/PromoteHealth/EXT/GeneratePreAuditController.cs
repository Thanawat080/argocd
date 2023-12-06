using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class GeneratePreAuditController : ControllerBase
    {
        private readonly IGeneratePreAuditServices extService;

        public GeneratePreAuditController(IGeneratePreAuditServices extService)
        {
            this.extService = extService;
        }
        [HttpGet("generate")]
        public async Task<bool> GenerateFile()
        {
           var result = await extService.GenerateFile();
            return result;
        }

        [HttpPost("ping")]
        public async Task<ActionResult<IEnumerable<PingModels>>> Ping(PingModels data)
        {
            Console.WriteLine("ping pong");
            var result = await extService.PingAi(data);
            return Ok(new { status = result, message = "ping success" });
        }

        [HttpGet("checkStatus/{jobId}")]
        public async Task<bool> CheckStatus(string jobId)
        {
            var result = await extService.ChekcStatus(int.Parse(jobId));
            return result;
        }
    }
}
