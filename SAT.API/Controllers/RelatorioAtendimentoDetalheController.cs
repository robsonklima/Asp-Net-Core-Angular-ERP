using System.Security.Claims;
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
    public class RelatorioAtendimentoDetalheController : ControllerBase
    {
        private readonly IRelatorioAtendimentoDetalheService _relatorioAtendimentoDetalheService;

        public RelatorioAtendimentoDetalheController(
            IRelatorioAtendimentoDetalheService relatorioAtendimentoDetalheService
        )
        {
            _relatorioAtendimentoDetalheService = relatorioAtendimentoDetalheService;
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public RelatorioAtendimentoDetalhe Post([FromBody] RelatorioAtendimentoDetalhe raDetalhe)
        {
            _relatorioAtendimentoDetalheService.Criar(raDetalhe);

            return raDetalhe;
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] RelatorioAtendimentoDetalhe raDetalhe)
        {
            raDetalhe.Acao = null;
            raDetalhe.Defeito = null;
            raDetalhe.TipoServico = null;
            raDetalhe.Causa = null;
            raDetalhe.TipoCausa = null;
            raDetalhe.GrupoCausa = null;
            raDetalhe.RelatorioAtendimentoDetalhePecas = null;
            _relatorioAtendimentoDetalheService.Atualizar(raDetalhe);
        }

        [HttpDelete("{codRATDetalhe}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codRATDetalhe)
        {
            _relatorioAtendimentoDetalheService.Deletar(codRATDetalhe);
        }
    }
}
