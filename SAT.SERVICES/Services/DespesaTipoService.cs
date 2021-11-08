using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaTipoService : IDespesaTipoService
    {
        private readonly IDespesaTipoRepository _despesaTipoRepo;

        public DespesaTipoService(IDespesaTipoRepository despesaTipoRepo)
        {
            _despesaTipoRepo = despesaTipoRepo;
        }

        public void Atualizar(DespesaTipo despesa)
        {
            _despesaTipoRepo.Atualizar(despesa);
        }

        public DespesaTipo Criar(DespesaTipo despesa)
        {
            _despesaTipoRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaTipoRepo.Deletar(codigo);
        }

        public DespesaTipo ObterPorCodigo(int codigo)
        {
            return _despesaTipoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaTipoParameters parameters)
        {
            var despesas = _despesaTipoRepo.ObterPorParametros(parameters);

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