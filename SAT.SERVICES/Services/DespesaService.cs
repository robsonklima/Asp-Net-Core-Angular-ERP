using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _despesaRepo;

        public DespesaService(IDespesaRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;
        }

        public void Atualizar(Despesa despesa)
        {
            _despesaRepo.Atualizar(despesa);
        }

        public Despesa Criar(Despesa despesa)
        {
            _despesaRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaRepo.Deletar(codigo);
        }

        public Despesa ObterPorCodigo(int codigo)
        {
            return _despesaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaParameters parameters)
        {
            var despesas = _despesaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesas,
                TotalCount = despesas.TotalCount,
                CurrentPage = despesas.CurrentPage,
                PageSize = despesas.PageSize,
                TotalPages = despesas.TotalPages,
                HasNext = despesas.HasNext,
                HasPrevious = despesas.HasPrevious
            };

            return lista;
        }
    }
}