using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPecaService
    {
        ListViewModel ObterPorParametros(PecaParameters parameters);
        Peca Criar(Peca peca);
        void Deletar(int codigo);
        void Atualizar(Peca peca);
        Peca ObterPorCodigo(int codigo);
        IActionResult ExportToExcel(PecaParameters parameters);
    }
}