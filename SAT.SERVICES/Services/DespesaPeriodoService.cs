using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaPeriodoService : IDespesaPeriodoService
    {
        private readonly IDespesaPeriodoRepository _despesaPeriodoRepo;

        public DespesaPeriodoService(IDespesaPeriodoRepository despesaPeriodoRepo)
        {
            _despesaPeriodoRepo = despesaPeriodoRepo;
        }

        public void Atualizar(DespesaPeriodo despesa)
        {
            _despesaPeriodoRepo.Atualizar(despesa);
        }

        public DespesaPeriodo Criar(DespesaPeriodo despesa)
        {
            _despesaPeriodoRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaPeriodoRepo.Deletar(codigo);
        }

        public DespesaPeriodo ObterPorCodigo(int codigo)
        {
            return _despesaPeriodoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaPeriodoParameters parameters)
        {
            var despesasPeriodo = _despesaPeriodoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesasPeriodo,
                TotalCount = despesasPeriodo.TotalCount,
                CurrentPage = despesasPeriodo.CurrentPage,
                PageSize = despesasPeriodo.PageSize,
                TotalPages = despesasPeriodo.TotalPages,
                HasNext = despesasPeriodo.HasNext,
                HasPrevious = despesasPeriodo.HasPrevious
            };

            return lista;
        }
    }
}