using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ProtocoloSTNService : IProtocoloSTNService
    {
        private readonly IProtocoloSTNRepository _ProtocoloSTNRepo;
        private readonly ISequenciaRepository _seqRepo;

        public ProtocoloSTNService(IProtocoloSTNRepository ProtocoloSTNRepo, ISequenciaRepository seqRepo)
        {
            _ProtocoloSTNRepo = ProtocoloSTNRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(ProtocoloSTNParameters parameters)
        {
            var ProtocoloSTNs = _ProtocoloSTNRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ProtocoloSTNs,
                TotalCount = ProtocoloSTNs.TotalCount,
                CurrentPage = ProtocoloSTNs.CurrentPage,
                PageSize = ProtocoloSTNs.PageSize,
                TotalPages = ProtocoloSTNs.TotalPages,
                HasNext = ProtocoloSTNs.HasNext,
                HasPrevious = ProtocoloSTNs.HasPrevious
            };

            return lista;
        }

        public ProtocoloSTN Criar(ProtocoloSTN protocoloSTN)
        {
            _ProtocoloSTNRepo.Criar(protocoloSTN);
            
            return protocoloSTN;
        }

        public void Deletar(int codigo)
        {
            _ProtocoloSTNRepo.Deletar(codigo);
        }

        public void Atualizar(ProtocoloSTN protocoloSTN)
        {
            _ProtocoloSTNRepo.Atualizar(protocoloSTN);
        }

        public ProtocoloSTN ObterPorCodigo(int codigo)
        {
            return _ProtocoloSTNRepo.ObterPorCodigo(codigo);
        }
    }
}
