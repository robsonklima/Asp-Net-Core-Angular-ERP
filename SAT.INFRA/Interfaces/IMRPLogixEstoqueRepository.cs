using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMRPLogixEstoqueRepository
    {
        MRPLogixEstoque Criar(MRPLogixEstoque mrpLogixEstoque);
        PagedList<MRPLogixEstoque> ObterPorParametros(MRPLogixEstoqueParameters parameters);
        void Deletar(int codigo);
        void Atualizar(MRPLogixEstoque mrpLogixEstoque);
        MRPLogixEstoque ObterPorCodigo(int codigo);
        void LimparTabela();        
    }
}
