using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORTransporteService : IORTransporteService
    {
        private readonly IORTransporteRepository _ORTransporteRepo;

        public ORTransporteService(IORTransporteRepository ORTransporteRepo)
        {
            _ORTransporteRepo = ORTransporteRepo;
        }

        public ListViewModel ObterPorParametros(ORTransporteParameters parameters)
        {
            var ORes = _ORTransporteRepo.ObterPorParametros(parameters);

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

        public ORTransporte Criar(ORTransporte ORTransporte)
        {
            _ORTransporteRepo.Criar(ORTransporte);

            return ORTransporte;
        }

        public void Deletar(int codigo)
        {
            _ORTransporteRepo.Deletar(codigo);
        }

        public void Atualizar(ORTransporte ORTransporte)
        {
            _ORTransporteRepo.Atualizar(ORTransporte);
        }

        public ORTransporte ObterPorCodigo(int codigo)
        {
            return _ORTransporteRepo.ObterPorCodigo(codigo);
        }
    }
}
