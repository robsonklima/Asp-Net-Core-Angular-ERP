using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITipoChamadoSTNService
    {
        ListViewModel ObterPorParametros(TipoChamadoSTNParameters parameters);
        TipoChamadoSTN ObterPorCodigo(int codigo);
        void Criar(TipoChamadoSTN tipoChamadoSTN);
        void Deletar(int codigoTipoChamadoSTN);
        void Atualizar(TipoChamadoSTN tipoChamadoSTN);
    }
}
