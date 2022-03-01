using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaConfiguracaoCombustivelService : IDespesaConfiguracaoCombustivelService
    {
        private readonly IDespesaConfiguracaoCombustivelRepository _despesaRepo;

        public DespesaConfiguracaoCombustivelService(IDespesaConfiguracaoCombustivelRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;
        }

        public void Atualizar(DespesaConfiguracaoCombustivel despesa)
        {
            _despesaRepo.Atualizar(despesa);
        }

        public DespesaConfiguracaoCombustivel Criar(DespesaConfiguracaoCombustivel despesa)
        {
            _despesaRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaRepo.Deletar(codigo);
        }

        public DespesaConfiguracaoCombustivel ObterPorCodigo(int codigo)
        {
            return _despesaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaConfiguracaoCombustivelParameters parameters)
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