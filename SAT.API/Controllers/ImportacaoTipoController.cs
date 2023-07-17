using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacaoTipoController : ControllerBase
    {
        private readonly IImportacaoTipoService _importacaoTipoService;

        public ImportacaoTipoController(IImportacaoTipoService importacaoTipoService)
        {
            _importacaoTipoService = importacaoTipoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ImportacaoTipoParameters parameters)
        {
            return _importacaoTipoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codImportacaoConf}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ImportacaoTipo Get(int codImportacaoTipo)
        {
            return _importacaoTipoService.ObterPorCodigo(codImportacaoTipo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public ImportacaoTipo Post([FromBody] ImportacaoTipo importacaoTipo)
        {
            return _importacaoTipoService.Criar(importacaoTipo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ImportacaoTipo importacaoTipo)
        {
            _importacaoTipoService.Atualizar(importacaoTipo);
        }

        [HttpDelete("{codImportacaoTipo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codImportacaoTipo)
        {
            _importacaoTipoService.Deletar(codImportacaoTipo);
        }
    }
}
