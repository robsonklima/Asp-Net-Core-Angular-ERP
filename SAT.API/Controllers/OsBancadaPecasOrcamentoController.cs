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
    public class OsBancadaPecasOrcamentoController : ControllerBase
    {
        private readonly IOsBancadaPecasOrcamentoService _OsBancadaPecasOrcamentoService;

        public OsBancadaPecasOrcamentoController(IOsBancadaPecasOrcamentoService OsBancadaPecasOrcamentoService)
        {
            _OsBancadaPecasOrcamentoService = OsBancadaPecasOrcamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OsBancadaPecasOrcamentoParameters parameters)
        {
            return _OsBancadaPecasOrcamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodOrcamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OsBancadaPecasOrcamento Get(int CodOrcamento)
        {
            return _OsBancadaPecasOrcamentoService.ObterPorCodigo(CodOrcamento);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OsBancadaPecasOrcamento Post([FromBody] OsBancadaPecasOrcamento OsBancadaPecasOrcamento)
        {
            return _OsBancadaPecasOrcamentoService.Criar(OsBancadaPecasOrcamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] OsBancadaPecasOrcamento OsBancadaPecasOrcamento)
        {
            _OsBancadaPecasOrcamentoService.Atualizar(OsBancadaPecasOrcamento);
        }

        [HttpDelete("{CodOrcamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcamento)
        {
            _OsBancadaPecasOrcamentoService.Deletar(codOrcamento);
        }
    }
}
