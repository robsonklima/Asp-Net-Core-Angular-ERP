using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaProtocoloPeriodoTecnicoService : IDespesaProtocoloPeriodoTecnicoService
    {
        private readonly IDespesaProtocoloPeriodoTecnicoRepository _despesaProtocoloRepo;

        public DespesaProtocoloPeriodoTecnicoService(IDespesaProtocoloPeriodoTecnicoRepository despesaProtocoloRepo)
        {
            _despesaProtocoloRepo = despesaProtocoloRepo;
        }

        public void Atualizar(DespesaProtocoloPeriodoTecnico protocolo)
        {
            _despesaProtocoloRepo.Atualizar(protocolo);
        }

        public DespesaProtocoloPeriodoTecnico Criar(DespesaProtocoloPeriodoTecnico protocolo)
        {
            _despesaProtocoloRepo.Criar(protocolo);

            return protocolo;
        }

        public void Deletar(int codigo)
        {
            _despesaProtocoloRepo.Deletar(codigo);
        }

        public DespesaProtocoloPeriodoTecnico ObterPorCodigo(int codigo)
        {
            return _despesaProtocoloRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaProtocoloPeriodoTecnicoParameters parameters)
        {
            var protocolos = _despesaProtocoloRepo.ObterPorParametros(parameters);

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
