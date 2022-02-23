using System.Collections.Generic;
using IISLogParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class IISLogController : ControllerBase
    {
        private readonly IIISLogService _iisLogService;

        public IISLogController(IIISLogService iisLogService)
        {
            _iisLogService = iisLogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public List<IISLogEvent> Get([FromQuery] IISLogParameters parameters)
        {
            return _iisLogService.Get(parameters);
        }
    }
}