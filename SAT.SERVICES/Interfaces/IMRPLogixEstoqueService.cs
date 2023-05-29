using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMRPLogixEstoqueService
    {
        ListViewModel ObterPorParametros(MRPLogixEstoqueParameters parameters);
        MRPLogixEstoque Criar(MRPLogixEstoque mrpLogix);
        void Deletar(int codigo);
        void Atualizar(MRPLogixEstoque mrpLogix);
        MRPLogixEstoque ObterPorCodigo(int codigo);
        void LimparTabela();        
    }
}
