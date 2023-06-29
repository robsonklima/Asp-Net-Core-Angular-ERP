using System;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IANSService
    {
        ListViewModel ObterPorParametros(ANSParameters parameters);
        ANS ObterPorCodigo(int codigo);
        ANS Criar(ANS ans);
        ANS Deletar(int codigo);
        ANS Atualizar(ANS ans);
        DateTime? CalcularPrazo(OrdemServico chamado);
    }
}
