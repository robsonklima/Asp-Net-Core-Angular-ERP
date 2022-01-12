using System.Collections.Generic;
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
    public class DeslocamentoController : ControllerBase
    {
        private readonly IDeslocamentoService _deslocamentoService;

        public DeslocamentoController(
            IDeslocamentoService deslocamentoService
        )
        {
            _deslocamentoService = deslocamentoService;
        }

        [HttpGet]
        public IEnumerable<Deslocamento> Get([FromQuery] DeslocamentoParameters parameters)
        {
            return _deslocamentoService.ObterPorParametros(parameters);
        }
    }
}