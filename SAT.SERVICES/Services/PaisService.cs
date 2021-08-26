using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisRepo;

        public PaisService(IPaisRepository paisRepo)
        {
            _paisRepo = paisRepo;
        }

        public ListViewModel ObterPorParametros(PaisParameters parameters)
        {
            var paises = _paisRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = paises,
                TotalCount = paises.TotalCount,
                CurrentPage = paises.CurrentPage,
                PageSize = paises.PageSize,
                TotalPages = paises.TotalPages,
                HasNext = paises.HasNext,
                HasPrevious = paises.HasPrevious
            };

            return lista;
        }

        public Pais Criar(Pais Pais)
        {
            _paisRepo.Criar(Pais);
            return Pais;
        }

        public void Deletar(int codigo)
        {
            _paisRepo.Deletar(codigo);
        }

        public void Atualizar(Pais pais)
        {
            _paisRepo.Atualizar(pais);
        }

        public Pais ObterPorCodigo(int codigo)
        {
            return _paisRepo.ObterPorCodigo(codigo);
        }
    }
}
