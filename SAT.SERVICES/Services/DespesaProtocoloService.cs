using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaProtocoloService : IDespesaProtocoloService
    {
        private readonly IDespesaProtocoloRepository _protocoloRepo;

        public DespesaProtocoloService(IDespesaProtocoloRepository protocoloRepo)
        {
            _protocoloRepo = protocoloRepo;
        }

        public void Atualizar(DespesaProtocolo despesa)
        {
            _protocoloRepo.Atualizar(despesa);
        }

        public DespesaProtocolo Criar(DespesaProtocolo protocolo)
        {
            protocolo =  _protocoloRepo.Criar(protocolo);

            protocolo.NomeProtocolo = $"{ protocolo.NomeProtocolo } - { protocolo.CodDespesaProtocolo }";
            _protocoloRepo.Atualizar(protocolo);

            return protocolo;
        }

        public void Deletar(int codigo)
        {
            _protocoloRepo.Deletar(codigo);
        }

        public DespesaProtocolo ObterPorCodigo(int codigo)
        {
            return _protocoloRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaProtocoloParameters parameters)
        {
            var protocolos = _protocoloRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = protocolos,
                TotalCount = protocolos.TotalCount,
                CurrentPage = protocolos.CurrentPage,
                PageSize = protocolos.PageSize,
                TotalPages = protocolos.TotalPages,
                HasNext = protocolos.HasNext,
                HasPrevious = protocolos.HasPrevious
            };

            return lista;
        }
    }
}