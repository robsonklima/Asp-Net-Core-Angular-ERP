using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    class RelatorioAtendimentoService : IRelatorioAtendimentoService
    {
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;

        public RelatorioAtendimentoService(IRelatorioAtendimentoRepository relatorioAtendimentoRepo)
        {
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
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
