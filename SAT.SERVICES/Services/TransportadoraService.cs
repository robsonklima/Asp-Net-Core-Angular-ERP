using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TransportadoraService : ITransportadoraService
    {
        private readonly ITransportadoraRepository _transportadoraRepo;

        public TransportadoraService(ITransportadoraRepository transportadoraRepo)
        {
            _transportadoraRepo = transportadoraRepo;
        }

        public ListViewModel ObterPorParametros(TransportadoraParameters parameters)
        {
            var transportadoras = _transportadoraRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = transportadoras,
                TotalCount = transportadoras.TotalCount,
                CurrentPage = transportadoras.CurrentPage,
                PageSize = transportadoras.PageSize,
                TotalPages = transportadoras.TotalPages,
                HasNext = transportadoras.HasNext,
                HasPrevious = transportadoras.HasPrevious
            };

            return lista;
        }

        public Transportadora Criar(Transportadora transportadora)
        {
            _transportadoraRepo.Criar(transportadora);
            return transportadora;
        }

        public void Deletar(int codigo)
        {
            _transportadoraRepo.Deletar(codigo);
        }

        public void Atualizar(Transportadora transportadora)
        {
            _transportadoraRepo.Atualizar(transportadora);
        }

        public Transportadora ObterPorCodigo(int codigo)
        {
            return _transportadoraRepo.ObterPorCodigo(codigo);
        }
    }
}
