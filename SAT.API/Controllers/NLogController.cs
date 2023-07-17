using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class NLogController : ControllerBase
    {
        private readonly INLogService _nLogService;

        public NLogController(INLogService nLogService)
        {
            _nLogService = nLogService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<NLogRegistro> Get([FromQuery] NLogParameters parameters)
        {
            return _nLogService.Obter(parameters);
        }

        // [HttpPost]
        // public NLogRegistro Post([FromBody] NLogRegistro log)
        // {
        //     return _nLogService.Criar(log);
        // }
    }
}
