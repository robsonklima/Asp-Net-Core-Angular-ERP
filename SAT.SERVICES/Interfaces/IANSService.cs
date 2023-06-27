using System;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IANSService
    {
        ListViewModel ObterPorParametros(ANSParameters parameters);
        ANS ObterPorCodigo(int codigo);
        void Criar(ANS ans);
        void Deletar(int codigo);
        void Atualizar(ANS ans);
        DateTime CalcularSLA(OrdemServico os);
    }
}
