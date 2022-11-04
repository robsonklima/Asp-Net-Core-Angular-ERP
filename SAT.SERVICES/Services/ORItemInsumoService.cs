using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORItemInsumoService : IORItemInsumoService
    {
        private readonly IORItemInsumoRepository _orItemInsumoRepo;

        public ORItemInsumoService(IORItemInsumoRepository orItemInsumoRepo)
        {
            _orItemInsumoRepo = orItemInsumoRepo;
        }

        public ListViewModel ObterPorParametros(ORItemInsumoParameters parameters)
        {
            var insumos = _orItemInsumoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = insumos,
                TotalCount = insumos.TotalCount,
                CurrentPage = insumos.CurrentPage,
                PageSize = insumos.PageSize,
                TotalPages = insumos.TotalPages,
                HasNext = insumos.HasNext,
                HasPrevious = insumos.HasPrevious
            };

            return lista;
        }

        public ORItemInsumo Criar(ORItemInsumo insumo)
        {
            return _orItemInsumoRepo.Criar(insumo);
        }

        public void Deletar(int codigo)
        {
            _orItemInsumoRepo.Deletar(codigo);
        }

        public ORItemInsumo Atualizar(ORItemInsumo insumo)
        {
            return _orItemInsumoRepo.Atualizar(insumo);
        }

        public ORItemInsumo ObterPorCodigo(int codigo)
        {
            return _orItemInsumoRepo.ObterPorCodigo(codigo);
        }
    }
}
