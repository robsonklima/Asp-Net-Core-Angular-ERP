using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaAdiantamentoPeriodoService : IDespesaAdiantamentoPeriodoService
    {
        private readonly IDespesaAdiantamentoPeriodoRepository _despesaAdiantamentoPeriodoRepo;

        public DespesaAdiantamentoPeriodoService(IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo)
        {
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
        }

        public void Atualizar(DespesaAdiantamentoPeriodo despesa)
        {
            _despesaAdiantamentoPeriodoRepo.Atualizar(despesa);
        }

        public DespesaAdiantamentoPeriodo Criar(DespesaAdiantamentoPeriodo despesa)
        {
            _despesaAdiantamentoPeriodoRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaAdiantamentoPeriodoRepo.Deletar(codigo);
        }

        public DespesaAdiantamentoPeriodo ObterPorCodigo(int codigo)
        {
            return _despesaAdiantamentoPeriodoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaAdiantamentoPeriodoParameters parameters)
        {
            var despesaAdiantamentoPeriodo = _despesaAdiantamentoPeriodoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesaAdiantamentoPeriodo,
                TotalCount = despesaAdiantamentoPeriodo.TotalCount,
                CurrentPage = despesaAdiantamentoPeriodo.CurrentPage,
                PageSize = despesaAdiantamentoPeriodo.PageSize,
                TotalPages = despesaAdiantamentoPeriodo.TotalPages,
                HasNext = despesaAdiantamentoPeriodo.HasNext,
                HasPrevious = despesaAdiantamentoPeriodo.HasPrevious
            };

            return lista;
        }
    }
}