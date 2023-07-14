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
    public class DocumentoSistemaController : ControllerBase
    {
        private readonly IDocumentoSistemaService _documentosistemaService;

        public DocumentoSistemaController(IDocumentoSistemaService documentosistemaService)
        {
            _documentosistemaService = documentosistemaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DocumentoSistemaParameters parameters)
        {
            return _documentosistemaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDocumentoSistema}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DocumentoSistema Get(int cod)
        {
            return _documentosistemaService.ObterPorCodigo(cod);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public DocumentoSistema Post([FromBody] DocumentoSistema documentosistema)
        {
            return this._documentosistemaService.Criar(documentosistema);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public DocumentoSistema Put([FromBody] DocumentoSistema documentosistema)
        {
            return this._documentosistemaService.Atualizar(documentosistema);
        }

        [HttpDelete("{codDocumentoSistema}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public DocumentoSistema Delete(int cod)
        {
            return this._documentosistemaService.Deletar(cod);
        }
    }
}
