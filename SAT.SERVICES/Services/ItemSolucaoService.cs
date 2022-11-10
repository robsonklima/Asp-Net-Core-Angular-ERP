using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ItemSolucaoService : IItemSolucaoService
    {
        private readonly IItemSolucaoRepository _ItemSolucaoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ItemSolucaoService(
            IItemSolucaoRepository ItemSolucaoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _ItemSolucaoRepo = ItemSolucaoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(ItemSolucaoParameters parameters)
        {
            var ItemSolucaos = _ItemSolucaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ItemSolucaos,
                TotalCount = ItemSolucaos.TotalCount,
                CurrentPage = ItemSolucaos.CurrentPage,
                PageSize = ItemSolucaos.PageSize,
                TotalPages = ItemSolucaos.TotalPages,
                HasNext = ItemSolucaos.HasNext,
                HasPrevious = ItemSolucaos.HasPrevious
            };

            return lista;
        }

        public ItemSolucao Criar(ItemSolucao itemSolucao)
        {
            _ItemSolucaoRepo.Criar(itemSolucao);

            return itemSolucao;
        }

        public void Deletar(int codigo)
        {
            _ItemSolucaoRepo.Deletar(codigo);
        }

        public void Atualizar(ItemSolucao itemSolucao)
        {
            _ItemSolucaoRepo.Atualizar(itemSolucao);
        }

        public ItemSolucao ObterPorCodigo(int codigo)
        {
            return _ItemSolucaoRepo.ObterPorCodigo(codigo);
        }
    }
}
