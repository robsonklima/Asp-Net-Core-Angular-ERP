using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _motivoRepo;

        public RegiaoService(IRegiaoRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(RegiaoParameters parameters)
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

        public Regiao Criar(Regiao regiao)
        {
            _motivoRepo.Criar(regiao);
            return regiao;
        }

        public void Deletar(int codigo)
        {
            _motivoRepo.Deletar(codigo);
        }

        public void Atualizar(Regiao regiao)
        {
            _motivoRepo.Atualizar(regiao);
        }

        public Regiao ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
