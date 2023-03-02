using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OsBancadaPecasOrcamentoService : IOsBancadaPecasOrcamentoService
    {
        private readonly IOsBancadaPecasOrcamentoRepository _osBancadaPecasOrcamentoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public OsBancadaPecasOrcamentoService(IOsBancadaPecasOrcamentoRepository osBancadaPecasOrcamentoRepo, ISequenciaRepository sequenciaRepo)
        {
            _osBancadaPecasOrcamentoRepo = osBancadaPecasOrcamentoRepo;
            this._sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(OsBancadaPecasOrcamento osBancadaPecasOrcamento)
        {
            this._osBancadaPecasOrcamentoRepo.Atualizar(osBancadaPecasOrcamento);
        }

        public OsBancadaPecasOrcamento Criar(OsBancadaPecasOrcamento osBancadaPecasOrcamento)
        {
            osBancadaPecasOrcamento.CodOrcamento = this._sequenciaRepo.ObterContador("OsbancadaPecasOrcamento");
            this._osBancadaPecasOrcamentoRepo.Criar(osBancadaPecasOrcamento);
            return osBancadaPecasOrcamento;
        }

        public void Deletar(int codigo)
        {
            this._osBancadaPecasOrcamentoRepo.Deletar(codigo);
        }

        public OsBancadaPecasOrcamento ObterPorCodigo(int codigo)
        {
            return this._osBancadaPecasOrcamentoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(OsBancadaPecasOrcamentoParameters parameters)
        {
            var osBancadaPecasOrcamentos = _osBancadaPecasOrcamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = osBancadaPecasOrcamentos,
                TotalCount = osBancadaPecasOrcamentos.TotalCount,
                CurrentPage = osBancadaPecasOrcamentos.CurrentPage,
                PageSize = osBancadaPecasOrcamentos.PageSize,
                TotalPages = osBancadaPecasOrcamentos.TotalPages,
                HasNext = osBancadaPecasOrcamentos.HasNext,
                HasPrevious = osBancadaPecasOrcamentos.HasPrevious
            };

            return lista;
        }
    }
}
