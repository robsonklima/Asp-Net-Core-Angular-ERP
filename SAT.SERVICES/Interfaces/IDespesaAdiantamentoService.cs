using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaAdiantamentoService
    {
        ListViewModel ObterPorParametros(DespesaAdiantamentoParameters parameters);
        DespesaAdiantamento Criar(DespesaAdiantamento despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaAdiantamento despesa);
        DespesaAdiantamento ObterPorCodigo(int codigo);
        List<ViewMediaDespesasAdiantamento> ObterMediaAdiantamentos(int codTecnico);
        DespesaAdiantamentoSolicitacao CriarSolicitacao(DespesaAdiantamentoSolicitacao solicitacao);
    }
}