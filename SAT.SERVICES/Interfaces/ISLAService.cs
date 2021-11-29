using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ISLAService
    {
        SLA Criar(SLA contrato);
        ListViewModel ObterPorParametros(SLAParameters parameters);
        void Deletar(int codigo);
        SLA Atualizar(SLA contrato);
        SLA ObterPorCodigo(int codigo);
    }
}
