using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoComunicacaoController : ControllerBase
    {
        private ITipoComunicacaoService _TipoComunicacaoService;

        public TipoComunicacaoController(
            ITipoComunicacaoService TipoComunicacaoService
        )
        {
            _TipoComunicacaoService = TipoComunicacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoComunicacaoParameters parameters)
        {
            return _TipoComunicacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoComunicacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TipoComunicacao Get(int codTipoComunicacao)
        {
            return _TipoComunicacaoService.ObterPorCodigo(codTipoComunicacao);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public TipoComunicacao Post([FromBody] TipoComunicacao tipo)
        {
            return _TipoComunicacaoService.Criar(tipo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public TipoComunicacao Put([FromBody] TipoComunicacao tipo)
        {
            return _TipoComunicacaoService.Atualizar(tipo);
        }

        [HttpDelete("{codTipoComunicacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public TipoComunicacao Delete(int codTipoComunicacao)
        {
            return _TipoComunicacaoService.Deletar(codTipoComunicacao);
        }
    }
}
