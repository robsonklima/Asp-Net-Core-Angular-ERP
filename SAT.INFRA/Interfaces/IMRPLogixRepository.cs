using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMRPLogixRepository
    {
        MRPLogix Criar(MRPLogix mrpLogix);
        PagedList<MRPLogix> ObterPorParametros(MRPLogixParameters parameters);
        void Deletar(int codigo);
        void Atualizar(MRPLogix mrpLogix);
        MRPLogix ObterPorCodigo(int codigo);
        void LimparTabela();        
    }
}
