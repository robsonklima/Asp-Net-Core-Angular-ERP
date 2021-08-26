using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepo;

        public CidadeService(ICidadeRepository cidadeRepo)
        {
            _cidadeRepo = cidadeRepo;
        }

        public ListViewModel ObterPorParametros(CidadeParameters parameters)
        {
            var cidades = _cidadeRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = cidades,
                TotalCount = cidades.TotalCount,
                CurrentPage = cidades.CurrentPage,
                PageSize = cidades.PageSize,
                TotalPages = cidades.TotalPages,
                HasNext = cidades.HasNext,
                HasPrevious = cidades.HasPrevious
            };

            return lista;
        }

        public Cidade Criar(Cidade cidade)
        {
            _cidadeRepo.Criar(cidade);
            return cidade;
        }

        public void Deletar(int codigo)
        {
            _cidadeRepo.Deletar(codigo);
        }

        public void Atualizar(Cidade cidade)
        {
            _cidadeRepo.Atualizar(cidade);
        }

        public Cidade ObterPorCodigo(int codigo)
        {
            return _cidadeRepo.ObterPorCodigo(codigo);
        }
    }
}
