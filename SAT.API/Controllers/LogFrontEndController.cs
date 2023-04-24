using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogFrontEndController : ControllerBase
    {
        private readonly ILogFrontEndService _logService;

        public LogFrontEndController(ILogFrontEndService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public void Post([FromBody] LogFrontEnd log)
        {
            _logService.Criar(log);
        }
    }
}
