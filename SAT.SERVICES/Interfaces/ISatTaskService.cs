using System;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ISatTaskService
    {
        ListViewModel ObterPorParametros(SatTaskParameters parameters);
        SatTask Criar(SatTask task);
        void Deletar(int codigo);
        void Atualizar(SatTask task);
        SatTask ObterPorCodigo(int codigo);
        Boolean PermitirExecucao(SatTaskTipoEnum tipo);
    }
}
