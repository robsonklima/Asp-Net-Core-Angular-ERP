using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoService
    {
        ListViewModel ObterPorParametros(OrcamentoParameters parameters);
        Orcamento Criar(Orcamento orcamento);
        void Deletar(int codigo);
        Orcamento Atualizar(Orcamento orcamento);
        Orcamento ObterPorCodigo(int codigo);
        ListViewModel ObterPorView(OrcamentoParameters parameters);
        OrcamentoAprovacao Aprovar(OrcamentoAprovacao aprovacao);
    }
}
