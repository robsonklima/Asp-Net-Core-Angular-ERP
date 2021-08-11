using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using System.Linq;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioAtendimentoController : ControllerBase
    {
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public RelatorioAtendimentoController(
            IRelatorioAtendimentoRepository relatorioAtendimentoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _relatorioAtendimentoInterface = relatorioAtendimentoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public RelatorioAtendimentoListViewModel Get([FromQuery] RelatorioAtendimentoParameters parameters)
        {
            var relatoriosAtendimento = _relatorioAtendimentoInterface.ObterPorParametros(parameters);

            var lista = new RelatorioAtendimentoListViewModel
            {
                RelatoriosAtendimento = relatoriosAtendimento,
                TotalCount = relatoriosAtendimento.TotalCount,
                CurrentPage = relatoriosAtendimento.CurrentPage,
                PageSize = relatoriosAtendimento.PageSize,
                TotalPages = relatoriosAtendimento.TotalPages,
                HasNext = relatoriosAtendimento.HasNext,
                HasPrevious = relatoriosAtendimento.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codRAT}")]
        public RelatorioAtendimento Get(int codRAT)
        {
            return _relatorioAtendimentoInterface.ObterPorCodigo(codRAT);
        }

        [HttpPost]
        public void Post([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            var codRAT = _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO);
            relatorioAtendimento.CodRAT = codRAT;

            for (int i = 0; i < relatorioAtendimento.RelatorioAtendimentoDetalhes.Count; i++)
            {
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].CodRATDetalhe = 
                    _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE);
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].Defeito = null;
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].TipoCausa = null;
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].TipoServico = null;
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].GrupoCausa = null;
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].Acao = null;
                relatorioAtendimento.RelatorioAtendimentoDetalhes[i].Causa = null;

                for (int c = 0; c < relatorioAtendimento.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas.Count; c++)
                {
                    relatorioAtendimento.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas[c].CodRATDetalhePeca = 
                        _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE_PECA);
                }
            }

            _relatorioAtendimentoInterface.Criar(relatorioAtendimento);
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimento relatorioAtendimento)
        {
            _relatorioAtendimentoInterface.Atualizar(relatorioAtendimento);
        }

        [HttpDelete("{codRAT}")]
        public void Delete(int codRAT)
        {
            _relatorioAtendimentoInterface.Deletar(codRAT);
        }
    }
}
