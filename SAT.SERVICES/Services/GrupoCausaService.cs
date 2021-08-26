using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class GrupoCausaService : IGrupoCausaService
    {
        private readonly IGrupoCausaRepository _grupoCausaRepo;

        public GrupoCausaService(IGrupoCausaRepository grupoCausaRepo)
        {
            _grupoCausaRepo = grupoCausaRepo;
        }

        public ListViewModel ObterPorParametros(GrupoCausaParameters parameters)
        {
            var grupoCausas = _grupoCausaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = grupoCausas,
                TotalCount = grupoCausas.TotalCount,
                CurrentPage = grupoCausas.CurrentPage,
                PageSize = grupoCausas.PageSize,
                TotalPages = grupoCausas.TotalPages,
                HasNext = grupoCausas.HasNext,
                HasPrevious = grupoCausas.HasPrevious
            };

            return lista;
        }

        public GrupoCausa Criar(GrupoCausa grupoCausa)
        {
            _grupoCausaRepo.Criar(grupoCausa);

            return grupoCausa;
        }

        public void Deletar(int codigo)
        {
            _grupoCausaRepo.Deletar(codigo);
        }

        public void Atualizar(GrupoCausa grupoCausa)
        {
            _grupoCausaRepo.Atualizar(grupoCausa);
        }

        public GrupoCausa ObterPorCodigo(int codigo)
        {
            return _grupoCausaRepo.ObterPorCodigo(codigo);
        }
    }
}
