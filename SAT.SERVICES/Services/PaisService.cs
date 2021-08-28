using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PaisService(IPaisRepository paisRepo, ISequenciaRepository seqRepo)
        {
            _paisRepo = paisRepo;
            _seqRepo = seqRepo;
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

        public Pais Criar(Pais pais)
        {
            pais.CodPais = _seqRepo.ObterContador("Pais");
            _paisRepo.Criar(pais);

            return pais;
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
