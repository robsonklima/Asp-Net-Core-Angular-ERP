using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IClientePecaGenericaService
    {
        ListViewModel ObterPorParametros(ClientePecaGenericaParameters parameters);
        ClientePecaGenerica Criar(ClientePecaGenerica peca);
        void Deletar(int codigo);
        void Atualizar(ClientePecaGenerica peca);
        ClientePecaGenerica ObterPorCodigo(int codigo);
        IActionResult ExportToExcel(ClientePecaGenericaParameters parameters);
    }
}