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
        private readonly IRelatorioAtendimentoRepository _raInterface;
        private readonly ISequenciaRepository _sequenciaInterface;
        private readonly IRelatorioAtendimentoDetalheRepository _raDetalheInterface;
        private readonly IRelatorioAtendimentoDetalhePecaRepository _raDetalhePecaInterface;

        public RelatorioAtendimentoController(
            IRelatorioAtendimentoRepository raInterface,
            ISequenciaRepository sequenciaInterface,
            IRelatorioAtendimentoDetalheRepository raDetalheInterface,
            IRelatorioAtendimentoDetalhePecaRepository raDetalhePecaInterface
        )
        {
            _raInterface = raInterface;
            _sequenciaInterface = sequenciaInterface;
            _raDetalheInterface = raDetalheInterface;
            _raDetalhePecaInterface = raDetalhePecaInterface;

        }

        [HttpGet]
        public RelatorioAtendimentoListViewModel Get([FromQuery] RelatorioAtendimentoParameters parameters)
        {
            var relatoriosAtendimento = _raInterface.ObterPorParametros(parameters);

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
            return _raInterface.ObterPorCodigo(codRAT);
        }

        [HttpPost]
        public void Post([FromBody] RelatorioAtendimento ra)
        {
            var codRAT = _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO);
            ra.CodRAT = codRAT;

            for (int i = 0; i < ra.RelatorioAtendimentoDetalhes.Count; i++)
            {
                ra.RelatorioAtendimentoDetalhes[i].CodRATDetalhe = 
                    _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE);
                ra.RelatorioAtendimentoDetalhes[i].Defeito = null;
                ra.RelatorioAtendimentoDetalhes[i].TipoCausa = null;
                ra.RelatorioAtendimentoDetalhes[i].TipoServico = null;
                ra.RelatorioAtendimentoDetalhes[i].GrupoCausa = null;
                ra.RelatorioAtendimentoDetalhes[i].Acao = null;
                ra.RelatorioAtendimentoDetalhes[i].Causa = null;

                for (int c = 0; c < ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas.Count; c++)
                {
                    ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas[c].CodRATDetalhePeca = 
                        _sequenciaInterface.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE_PECA);
                }
            }

            _raInterface.Criar(ra);
        }

        [HttpPut]
        public void Put([FromBody] RelatorioAtendimento ra)
        {
            var raOriginal = _raInterface.ObterPorCodigo(ra.CodRAT);

            // Remover todos os antigos detalhes
            foreach (var d in raOriginal.RelatorioAtendimentoDetalhes.ToList())
            {
                foreach (var rp in d.RelatorioAtendimentoDetalhePecas)
                {
                    _raDetalhePecaInterface.Deletar(rp.CodRATDetalhePeca);
                }

                _raDetalheInterface.Deletar(d.CodRATDetalhe);
            }

            // Inserir todos os novos detalhes
            for (int i = 0; i < ra.RelatorioAtendimentoDetalhes.Count; i++)
            {
                ra.RelatorioAtendimentoDetalhes[i].CodRATDetalhe = _sequenciaInterface
                    .ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE);
                ra.RelatorioAtendimentoDetalhes[i].CodRAT = ra.CodRAT;
                ra.RelatorioAtendimentoDetalhes[i].Defeito = null;
                ra.RelatorioAtendimentoDetalhes[i].TipoCausa = null;
                ra.RelatorioAtendimentoDetalhes[i].TipoServico = null;
                ra.RelatorioAtendimentoDetalhes[i].GrupoCausa = null;
                ra.RelatorioAtendimentoDetalhes[i].Acao = null;
                ra.RelatorioAtendimentoDetalhes[i].Causa = null;

                for (int c = 0; c < ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas.Count; c++)
                {
                    ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas[c].CodRATDetalhePeca = _sequenciaInterface
                        .ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO_DETALHE_PECA);
                    ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas[c].CodRATDetalhe = ra.RelatorioAtendimentoDetalhes[i].CodRATDetalhe;

                    _raDetalhePecaInterface.Criar(ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas[c]);
                }

                ra.RelatorioAtendimentoDetalhes[i].RelatorioAtendimentoDetalhePecas = null;
                _raDetalheInterface.Criar(ra.RelatorioAtendimentoDetalhes[i]);
            }

            _raInterface.Atualizar(ra);
        }

        [HttpDelete("{codRAT}")]
        public void Delete(int codRAT)
        {
            _raInterface.Deletar(codRAT);
        }
    }
}
