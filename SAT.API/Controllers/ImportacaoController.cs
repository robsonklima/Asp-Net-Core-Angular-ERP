using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ImportacaoController : ControllerBase
    {
        private IImportacaoService _importacaoService;
        public ImportacaoController(IImportacaoService importacaoService)
        {
            _importacaoService = importacaoService;

        }

        [HttpPost("AberturaOs")]
        public List<string> Post([FromBody] ImportacaoBase importacao)
        {

            return _importacaoService.Importacao(importacao);
        }
    }
}
