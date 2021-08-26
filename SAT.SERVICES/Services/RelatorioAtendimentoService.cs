using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoService : IRelatorioAtendimentoService
    {
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public RelatorioAtendimentoService(IRelatorioAtendimentoRepository relatorioAtendimentoRepo, ISequenciaRepository seqRepo)
        {
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
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
            relatorioAtendimento.CodRAT = _seqRepo.ObterContador(Constants.TABELA_RELATORIO_ATENDIMENTO);
            relatorioAtendimento.RelatorioAtendimentoDetalhes = null;
            _relatorioAtendimentoRepo.Criar(relatorioAtendimento);

            return relatorioAtendimento;
        }

        public void Deletar(int codigo)
        {
            _relatorioAtendimentoRepo.Deletar(codigo);
        }

        public void Atualizar(RelatorioAtendimento relatorioAtendimento)
        {
            _relatorioAtendimentoRepo.Atualizar(relatorioAtendimento);
        }

        public RelatorioAtendimento ObterPorCodigo(int codigo)
        {
            return _relatorioAtendimentoRepo.ObterPorCodigo(codigo);
        }
    }
}
