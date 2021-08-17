using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioAtendimentoDetalheController : ControllerBase
    {
        private readonly IRelatorioAtendimentoDetalheRepository _relatorioAtendimentoDetalheInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public RelatorioAtendimentoDetalheController(
            IRelatorioAtendimentoDetalheRepository relatorioAtendimentoDetalheInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _relatorioAtendimentoDetalheInterface = relatorioAtendimentoDetalheInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpPost]
        public RelatorioAtendimentoDetalhe Post([FromBody] RelatorioAtendimentoDetalhe raDetalhe)
        {
            raDetalhe.CodRATDetalhe = _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE);
            raDetalhe.Acao = null;
            raDetalhe.Defeito = null;
            raDetalhe.TipoServico = null;
            raDetalhe.Causa = null;
            raDetalhe.TipoCausa = null;
            raDetalhe.GrupoCausa = null;
            raDetalhe.RelatorioAtendimentoDetalhePecas = null;
            _relatorioAtendimentoDetalheInterface.Criar(raDetalhe);
            return raDetalhe;
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimentoDetalhe raDetalhe)
        {
            raDetalhe.Acao = null;
            raDetalhe.Defeito = null;
            raDetalhe.TipoServico = null;
            raDetalhe.Causa = null;
            raDetalhe.TipoCausa = null;
            raDetalhe.GrupoCausa = null;
            raDetalhe.RelatorioAtendimentoDetalhePecas = null;
            _relatorioAtendimentoDetalheInterface.Atualizar(raDetalhe);
        }

        [HttpDelete("{codRATDetalhe}")]
        public void Delete(int codRATDetalhe)
        {
            _relatorioAtendimentoDetalheInterface.Deletar(codRATDetalhe);
        }
    }
}
