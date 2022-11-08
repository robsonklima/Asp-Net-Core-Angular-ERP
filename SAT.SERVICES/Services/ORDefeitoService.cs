using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORDefeitoService : IORDefeitoService
    {
        private readonly IORDefeitoRepository _ORDefeitoRepo;

        public ORDefeitoService(IORDefeitoRepository ORDefeitoRepo)
        {
            _ORDefeitoRepo = ORDefeitoRepo;
        }

        public ListViewModel ObterPorParametros(ORDefeitoParameters parameters)
        {
            var ORes = _ORDefeitoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ORes,
                TotalCount = ORes.TotalCount,
                CurrentPage = ORes.CurrentPage,
                PageSize = ORes.PageSize,
                TotalPages = ORes.TotalPages,
                HasNext = ORes.HasNext,
                HasPrevious = ORes.HasPrevious
            };

            return lista;
        }

        public ORDefeito Criar(ORDefeito orDefeito)
        {
            _ORDefeitoRepo.Criar(orDefeito);

            return orDefeito;
        }

        public void Deletar(int codigo)
        {
            _ORDefeitoRepo.Deletar(codigo);
        }

        public void Atualizar(ORDefeito orDefeito)
        {
            _ORDefeitoRepo.Atualizar(orDefeito);
        }

        public ORDefeito ObterPorCodigo(int codigo)
        {
            return _ORDefeitoRepo.ObterPorCodigo(codigo);
        }
    }
}
