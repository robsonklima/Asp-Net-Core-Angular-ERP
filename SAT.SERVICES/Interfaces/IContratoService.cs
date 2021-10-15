using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoService
    {
        ListViewModel ObterPorParametros(ContratoParameters parameters);
        Contrato Criar(Contrato contrato);
        void Deletar(int codigo);
        void Atualizar(Contrato contrato);
        IActionResult ExportToExcel(ContratoParameters parameters);
    }
}
