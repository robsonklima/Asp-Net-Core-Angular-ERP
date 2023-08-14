using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacaoAdendoController : ControllerBase
    {
        private IImportacaoAdendoService _ImportacaoAdendoService;

        public ImportacaoAdendoController(IImportacaoAdendoService ImportacaoAdendoService)
        {
            _ImportacaoAdendoService = ImportacaoAdendoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ImportacaoAdendoParameters parameters)
        {
            return _ImportacaoAdendoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodImportacaoAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ImportacaoAdendo Get(int CodImportacaoAdendo)
        {
            return _ImportacaoAdendoService.ObterPorCodigo(CodImportacaoAdendo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public ImportacaoAdendo Post([FromBody] ImportacaoAdendo adendo)
        {
            return _ImportacaoAdendoService.Criar(adendo);
        }

        [HttpPut("{codImportacaoAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public ImportacaoAdendo Put(int codImportacaoAdendo, [FromBody] ImportacaoAdendo adendo)
        {
            return _ImportacaoAdendoService.Atualizar(adendo);
        }

        [HttpDelete("{CodImportacaoAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public ImportacaoAdendo Delete(int CodImportacaoAdendo)
        {
            return _ImportacaoAdendoService.Deletar(CodImportacaoAdendo);
        }
    }
}
