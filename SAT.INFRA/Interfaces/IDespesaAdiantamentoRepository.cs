using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaAdiantamentoRepository
    {
        PagedList<DespesaAdiantamento> ObterPorParametros(DespesaAdiantamentoParameters parameters);
        void Criar(DespesaAdiantamento despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaAdiantamento despesa);
        DespesaAdiantamento ObterPorCodigo(int codigo);
        List<ViewMediaDespesasAdiantamento> ObterMediaAdiantamentos(int codTecnico);
        DespesaAdiantamentoSolicitacao CriarSolicitacao(DespesaAdiantamentoSolicitacao solicitacao);
    }
}