using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORTempoReparoService : IORTempoReparoService
    {
        private readonly IORTempoReparoRepository _orTempoReparoRepo;

        public ORTempoReparoService(IORTempoReparoRepository orTempoReparoRepo)
        {
            _orTempoReparoRepo = orTempoReparoRepo;
        }

        public ListViewModel ObterPorParametros(ORTempoReparoParameters parameters)
        {
            var d = _orTempoReparoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = d,
                TotalCount = d.TotalCount,
                CurrentPage = d.CurrentPage,
                PageSize = d.PageSize,
                TotalPages = d.TotalPages,
                HasNext = d.HasNext,
                HasPrevious = d.HasPrevious
            };

            return lista;
        }

        public ORTempoReparo Criar(ORTempoReparo tr)
        {
            _orTempoReparoRepo.Criar(tr);

            return tr;
        }

        public void Deletar(int codigo)
        {
            _orTempoReparoRepo.Deletar(codigo);
        }

        public void Atualizar(ORTempoReparo tr)
        {
            _orTempoReparoRepo.Atualizar(tr);
        }

        public ORTempoReparo ObterPorCodigo(int codigo)
        {
            return _orTempoReparoRepo.ObterPorCodigo(codigo);
        }
    }
}
