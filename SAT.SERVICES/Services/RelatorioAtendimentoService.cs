using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoService : IRelatorioAtendimentoService
    {
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly IRelatorioAtendimentoDetalheRepository _relatorioAtendimentoDetalheRepo;
        private readonly IRelatorioAtendimentoDetalhePecaRepository _relatorioAtendimentoDetalhePecaRepo;
        private readonly ISequenciaRepository _seqRepo;

        public RelatorioAtendimentoService(
                IRelatorioAtendimentoRepository relatorioAtendimentoRepo,
                IRelatorioAtendimentoDetalheRepository relatorioAtendimentoDetalheRepo,
                IRelatorioAtendimentoDetalhePecaRepository relatorioAtendimentoDetalhePecaRepo,
                ISequenciaRepository seqRepo)
        {
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
            _relatorioAtendimentoDetalheRepo = relatorioAtendimentoDetalheRepo;
            _relatorioAtendimentoDetalhePecaRepo = relatorioAtendimentoDetalhePecaRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(RelatorioAtendimentoParameters parameters)
        {
            var relatorios = _relatorioAtendimentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = relatorios,
                TotalCount = relatorios.TotalCount,
                CurrentPage = relatorios.CurrentPage,
                PageSize = relatorios.PageSize,
                TotalPages = relatorios.TotalPages,
                HasNext = relatorios.HasNext,
                HasPrevious = relatorios.HasPrevious
            };

            return lista;
        }

        public RelatorioAtendimento Criar(RelatorioAtendimento relatorioAtendimento)
        {
            relatorioAtendimento.CodRAT = _seqRepo.ObterContador("RAT");

            if (relatorioAtendimento.NumRAT == null)
            {
                relatorioAtendimento.NumRAT = relatorioAtendimento.CodRAT.ToString() + "-E";
            }

            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _relatorioAtendimentoRepo.Criar(relatorioAtendimento);

            return relatorioAtendimento;
        }

        public RelatorioAtendimento Atualizar(RelatorioAtendimento relatorioAtendimento)
        {

            if (relatorioAtendimento.NumRAT == null)
            {
                relatorioAtendimento.NumRAT = relatorioAtendimento.CodRAT.ToString() + "-E";
            }

            _relatorioAtendimentoRepo.Atualizar(relatorioAtendimento);

            return relatorioAtendimento;
        }

        public RelatorioAtendimento ObterPorCodigo(int codigo)
        {
            return _relatorioAtendimentoRepo.ObterPorCodigo(codigo);
        }

        public bool Deletar(int codigo)
        {
            try
            {
                var relatorio = _relatorioAtendimentoRepo.ObterPorCodigo(codigo);

                if (relatorio.RelatorioAtendimentoDetalhes.Any())
                {
                    relatorio
                        .RelatorioAtendimentoDetalhes.ToList()
                            .ForEach(detalhe =>
                            {
                                if (detalhe.RelatorioAtendimentoDetalhePecas.Any())
                                {
                                    detalhe
                                        .RelatorioAtendimentoDetalhePecas.ToList()
                                            .ForEach(peca =>
                                            {
                                                _relatorioAtendimentoDetalhePecaRepo.Deletar(peca.CodRATDetalhePeca);
                                            });
                                }

                                _relatorioAtendimentoDetalheRepo.Deletar(detalhe.CodRATDetalhe);
                            });
                }

                _relatorioAtendimentoRepo.Deletar(codigo);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
