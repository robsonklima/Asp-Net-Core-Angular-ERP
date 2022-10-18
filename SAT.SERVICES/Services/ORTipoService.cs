using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORTipoService : IORTipoService
    {
        private readonly IORTipoRepository _ORTipoRepo;

        public ORTipoService(IORTipoRepository ORTipoRepo)
        {
            _ORTipoRepo = ORTipoRepo;
        }

        public ListViewModel ObterPorParametros(ORTipoParameters parameters)
        {
            var tipos = _ORTipoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipos,
                TotalCount = tipos.TotalCount,
                CurrentPage = tipos.CurrentPage,
                PageSize = tipos.PageSize,
                TotalPages = tipos.TotalPages,
                HasNext = tipos.HasNext,
                HasPrevious = tipos.HasPrevious
            };

            return lista;
        }

        public ORTipo Criar(ORTipo tipo)
        {
            _ORTipoRepo.Criar(tipo);

            return tipo;
        }

        public void Deletar(int codigo)
        {
            _ORTipoRepo.Deletar(codigo);
        }

        public void Atualizar(ORTipo tipo)
        {
            _ORTipoRepo.Atualizar(tipo);
        }

        public ORTipo ObterPorCodigo(int codigo)
        {
            return _ORTipoRepo.ObterPorCodigo(codigo);
        }
    }
}
