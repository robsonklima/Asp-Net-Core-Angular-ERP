using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoPecasEspecService : IOrcamentoPecasEspecService
    {
        private readonly IOrcamentoPecasEspecRepository _orcamentoPecasEspecRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public OrcamentoPecasEspecService(IOrcamentoPecasEspecRepository orcamentoPecasEspecRepo, ISequenciaRepository sequenciaRepo)
        {
            _orcamentoPecasEspecRepo = orcamentoPecasEspecRepo;
            this._sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(OrcamentoPecasEspec orcamentoPecasEspec)
        {
            this._orcamentoPecasEspecRepo.Atualizar(orcamentoPecasEspec);
        }

        public OrcamentoPecasEspec Criar(OrcamentoPecasEspec orcamentoPecasEspec)
        {
            orcamentoPecasEspec.CodOsbancada = this._sequenciaRepo.ObterContador("OrcamentoPecasEspec");
            this._orcamentoPecasEspecRepo.Criar(orcamentoPecasEspec);
            return orcamentoPecasEspec;
        }

        public void Deletar(int codigo)
        {
            this._orcamentoPecasEspecRepo.Deletar(codigo);
        }

        public OrcamentoPecasEspec ObterPorCodigo(int codigo)
        {
            return this._orcamentoPecasEspecRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(OrcamentoPecasEspecParameters parameters)
        {
            var orcamentoPecasEspecs = _orcamentoPecasEspecRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentoPecasEspecs,
                TotalCount = orcamentoPecasEspecs.TotalCount,
                CurrentPage = orcamentoPecasEspecs.CurrentPage,
                PageSize = orcamentoPecasEspecs.PageSize,
                TotalPages = orcamentoPecasEspecs.TotalPages,
                HasNext = orcamentoPecasEspecs.HasNext,
                HasPrevious = orcamentoPecasEspecs.HasPrevious
            };

            return lista;
        }
    }
}
