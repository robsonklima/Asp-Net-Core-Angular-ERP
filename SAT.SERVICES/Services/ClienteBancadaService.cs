using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ClienteBancadaService : IClienteBancadaService
    {
        private readonly IClienteBancadaRepository _clienteBancadaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ClienteBancadaService(IClienteBancadaRepository clienteBancadaRepo, ISequenciaRepository sequenciaRepo)
        {
            _clienteBancadaRepo = clienteBancadaRepo;
            this._sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(ClienteBancada clienteBancada)
        {
            this._clienteBancadaRepo.Atualizar(clienteBancada);
        }

        public ClienteBancada Criar(ClienteBancada clienteBancada)
        {
            clienteBancada.CodClienteBancada = this._sequenciaRepo.ObterContador("ClienteBancada");
            this._clienteBancadaRepo.Criar(clienteBancada);
            return clienteBancada;
        }

        public void Deletar(int codigo)
        {
            this._clienteBancadaRepo.Deletar(codigo);
        }

        public ClienteBancada ObterPorCodigo(int codigo)
        {
            return this._clienteBancadaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ClienteBancadaParameters parameters)
        {
            var clienteBancadas = _clienteBancadaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clienteBancadas,
                TotalCount = clienteBancadas.TotalCount,
                CurrentPage = clienteBancadas.CurrentPage,
                PageSize = clienteBancadas.PageSize,
                TotalPages = clienteBancadas.TotalPages,
                HasNext = clienteBancadas.HasNext,
                HasPrevious = clienteBancadas.HasPrevious
            };

            return lista;
        }
    }
}
