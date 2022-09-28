using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoRepository
    {
        PagedList<Orcamento> ObterPorParametros(OrcamentoParameters parameters);
        void Criar(Orcamento orcamento);
        void Atualizar(Orcamento orcamento);
        void Deletar(int codOrcamento);
        Orcamento ObterPorCodigo(int codigo);
        PagedList<ViewOrcamentoLista> ObterPorView(OrcamentoParameters parameters);
    }
}
