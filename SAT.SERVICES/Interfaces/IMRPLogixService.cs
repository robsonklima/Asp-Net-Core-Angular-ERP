using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMRPLogixService
    {
        ListViewModel ObterPorParametros(MRPLogixParameters parameters);
        MRPLogix Criar(MRPLogix mrpLogix);
        void Deletar(int codigo);
        void Atualizar(MRPLogix mrpLogix);
        MRPLogix ObterPorCodigo(int codigo);
    }
}
