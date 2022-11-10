using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ItemDefeitoService : IItemDefeitoService
    {
        private readonly IItemDefeitoRepository _ItemDefeitoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ItemDefeitoService(
            IItemDefeitoRepository ItemDefeitoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _ItemDefeitoRepo = ItemDefeitoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(ItemDefeitoParameters parameters)
        {
            var ItemDefeitos = _ItemDefeitoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ItemDefeitos,
                TotalCount = ItemDefeitos.TotalCount,
                CurrentPage = ItemDefeitos.CurrentPage,
                PageSize = ItemDefeitos.PageSize,
                TotalPages = ItemDefeitos.TotalPages,
                HasNext = ItemDefeitos.HasNext,
                HasPrevious = ItemDefeitos.HasPrevious
            };

            return lista;
        }

        public ItemDefeito Criar(ItemDefeito itemDefeito)
        {
            _ItemDefeitoRepo.Criar(itemDefeito);

            return itemDefeito;
        }

        public void Deletar(int codigo)
        {
            _ItemDefeitoRepo.Deletar(codigo);
        }

        public void Atualizar(ItemDefeito itemDefeito)
        {
            _ItemDefeitoRepo.Atualizar(itemDefeito);
        }

        public ItemDefeito ObterPorCodigo(int codigo)
        {
            return _ItemDefeitoRepo.ObterPorCodigo(codigo);
        }
    }
}
