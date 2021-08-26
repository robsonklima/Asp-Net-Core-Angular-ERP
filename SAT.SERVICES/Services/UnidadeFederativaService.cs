using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class UnidadeFederativaService : IUnidadeFederativaService
    {
        private readonly IUnidadeFederativaRepository _unidadeFederativaRepo;

        public UnidadeFederativaService(IUnidadeFederativaRepository unidadeFederativaRepo)
        {
            _unidadeFederativaRepo = unidadeFederativaRepo;
        }

        public ListViewModel ObterPorParametros(UnidadeFederativaParameters parameters)
        {
            var ufs = _unidadeFederativaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ufs,
                TotalCount = ufs.TotalCount,
                CurrentPage = ufs.CurrentPage,
                PageSize = ufs.PageSize,
                TotalPages = ufs.TotalPages,
                HasNext = ufs.HasNext,
                HasPrevious = ufs.HasPrevious
            };

            return lista;
        }

        public UnidadeFederativa Criar(UnidadeFederativa unidadeFederativa)
        {
            _unidadeFederativaRepo.Criar(unidadeFederativa);
            return unidadeFederativa;
        }

        public void Deletar(int codigo)
        {
            _unidadeFederativaRepo.Deletar(codigo);
        }

        public void Atualizar(UnidadeFederativa unidadeFederativa)
        {
            _unidadeFederativaRepo.Atualizar(unidadeFederativa);
        }

        public UnidadeFederativa ObterPorCodigo(int codigo)
        {
            return _unidadeFederativaRepo.ObterPorCodigo(codigo);
        }
    }
}
