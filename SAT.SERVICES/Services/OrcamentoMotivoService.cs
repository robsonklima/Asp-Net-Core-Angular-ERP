using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoMotivoService : IOrcamentoMotivoService
    {
        private readonly IOrcamentoMotivoRepository _orcamentoMotivoRepo;

        public OrcamentoMotivoService(IOrcamentoMotivoRepository orcamentoMotivoRepo)
        {
            _orcamentoMotivoRepo = orcamentoMotivoRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoMotivoParameters parameters)
        {
            var orcamentoMotivos = _orcamentoMotivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentoMotivos,
                TotalCount = orcamentoMotivos.TotalCount,
                CurrentPage = orcamentoMotivos.CurrentPage,
                PageSize = orcamentoMotivos.PageSize,
                TotalPages = orcamentoMotivos.TotalPages,
                HasNext = orcamentoMotivos.HasNext,
                HasPrevious = orcamentoMotivos.HasPrevious
            };

            return lista;
        }

        public OrcamentoMotivo Criar(OrcamentoMotivo orcamentoMotivo)
        {
            _orcamentoMotivoRepo.Criar(orcamentoMotivo);
            return orcamentoMotivo;
        }

        public void Deletar(int codigo)
        {
            _orcamentoMotivoRepo.Deletar(codigo);
        }

        public void Atualizar(OrcamentoMotivo orcamentoMotivo)
        {
            _orcamentoMotivoRepo.Atualizar(orcamentoMotivo);
        }

        public OrcamentoMotivo ObterPorCodigo(int codigo)
        {
            return _orcamentoMotivoRepo.ObterPorCodigo(codigo);
        }
    }
}
