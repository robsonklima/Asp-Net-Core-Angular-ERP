using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RedeBanrisulService : IRedeBanrisulService
    {
        private readonly IRedeBanrisulRepository _motivoRepo;

        public RedeBanrisulService(IRedeBanrisulRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(RedeBanrisulParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public RedeBanrisul Criar(RedeBanrisul RedeBanrisul)
        {
            _motivoRepo.Criar(RedeBanrisul);

            return RedeBanrisul;
        }

        public RedeBanrisul Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public RedeBanrisul Atualizar(RedeBanrisul RedeBanrisul)
        {
            return _motivoRepo.Atualizar(RedeBanrisul);
        }

        public RedeBanrisul ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
