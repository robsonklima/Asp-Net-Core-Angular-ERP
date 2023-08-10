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
    public class ImportacaoArquivoController : ControllerBase
    {
        private readonly IImportacaoArquivoService _importacaoArquivoService;

        public ImportacaoArquivoController(IImportacaoArquivoService importacaoArquivoService)
        {
            _importacaoArquivoService = importacaoArquivoService;
        }

        [HttpPost]
//        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ImportacaoArquivo importacaoArquivo)
        {
            _importacaoArquivoService.Criar(importacaoArquivo);
        }

    }
}