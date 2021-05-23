using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using Elfo.Round.Mvc;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Controllers
{
    [Route("api/warmup")]
    public class WarmupController : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> Get([FromServices]ILogger logger)
        {
            logger.Information("Warmup started");
            // Execute Warmup actions here
            logger.Information("Warmup ended");
            return Task.FromResult(this.ResultOk() as IActionResult);
        }
    }
}
