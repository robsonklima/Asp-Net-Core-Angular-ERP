using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RecursoBloqueadoService : IRecursoBloqueadoService
    {
        private readonly IRecursoBloqueadoRepository _recursoBloqueadoRepo;

        public RecursoBloqueadoService(IRecursoBloqueadoRepository recursoBloqueadoRepo)
        {
            _recursoBloqueadoRepo = recursoBloqueadoRepo;
        }

        public ListViewModel ObterPorParametros(RecursoBloqueadoParameters parameters)
        {
            var data = _recursoBloqueadoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }

        public RecursoBloqueado Criar(RecursoBloqueado RecursoBloqueado)
        {
            _recursoBloqueadoRepo.Criar(RecursoBloqueado);

            return RecursoBloqueado;
        }

        public RecursoBloqueado Deletar(int codigo)
        {
            return _recursoBloqueadoRepo.Deletar(codigo);
        }

        public RecursoBloqueado Atualizar(RecursoBloqueado RecursoBloqueado)
        {
            _recursoBloqueadoRepo.Atualizar(RecursoBloqueado);

            return RecursoBloqueado;
        }

        public RecursoBloqueado ObterPorCodigo(int codigo)
        {
            return _recursoBloqueadoRepo.ObterPorCodigo(codigo);
        }
    }
}
