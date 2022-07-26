using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPecaStatusService
    {
        ListViewModel ObterPorParametros(PecaStatusParameters parameters);
        PecaStatus Criar(PecaStatus pecaStatus);
        void Deletar(int codigo);
        void Atualizar(PecaStatus pecaStatus);
        PecaStatus ObterPorCodigo(int codigo);
        IActionResult ExportToExcel(PecaStatusParameters parameters);
    }
}