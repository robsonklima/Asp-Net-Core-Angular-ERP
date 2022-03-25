using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IClientePecaService
    {
        ListViewModel ObterPorParametros(ClientePecaParameters parameters);
        ClientePeca Criar(ClientePeca peca);
        void Deletar(int codigo);
        void Atualizar(ClientePeca peca);
        ClientePeca ObterPorCodigo(int codigo);
        IActionResult ExportToExcel(ClientePecaParameters parameters);
    }
}