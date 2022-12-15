using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ProtocoloChamadoSTNService : IProtocoloChamadoSTNService
    {
        private readonly IProtocoloChamadoSTNRepository _protocoloChamadoSTNRepo;

        public ProtocoloChamadoSTNService(
            IProtocoloChamadoSTNRepository protocoloChamadoSTNRepo
        )
        {
            _protocoloChamadoSTNRepo = protocoloChamadoSTNRepo;
        }

        public void Atualizar(ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            _protocoloChamadoSTNRepo.Atualizar(protocoloChamadoSTN);
        }

        public void Criar(ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            _protocoloChamadoSTNRepo.Criar(protocoloChamadoSTN);
        }

        public void Deletar(int codProtocoloChamadoSTN)
        {
            _protocoloChamadoSTNRepo.Deletar(codProtocoloChamadoSTN);
        }

        public ProtocoloChamadoSTN ObterPorCodigo(int codProtocoloChamadoSTN)
        {
            return _protocoloChamadoSTNRepo.ObterPorCodigo(codProtocoloChamadoSTN);
        }

        public ListViewModel ObterPorParametros(ProtocoloChamadoSTNParameters parameters)
        {
            var protocoloChamadoSTNs = _protocoloChamadoSTNRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = protocoloChamadoSTNs,
                TotalCount = protocoloChamadoSTNs.TotalCount,
                CurrentPage = protocoloChamadoSTNs.CurrentPage,
                PageSize = protocoloChamadoSTNs.PageSize,
                TotalPages = protocoloChamadoSTNs.TotalPages,
                HasNext = protocoloChamadoSTNs.HasNext,
                HasPrevious = protocoloChamadoSTNs.HasPrevious
            };

            return lista;
        }

    }
}
