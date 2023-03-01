using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoService
    {
        ListViewModel ObterPorParametros(ContratoParameters parameters);
        Contrato Criar(Contrato contrato);
        void Deletar(int codigo);
        Contrato Atualizar(Contrato contrato);
        Contrato ObterPorCodigo(int codigo);
    }
}
