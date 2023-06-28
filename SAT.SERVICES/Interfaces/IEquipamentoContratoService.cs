using System;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IEquipamentoContratoService
    {
        ListViewModel ObterPorParametros(EquipamentoContratoParameters parameters);
        EquipamentoContrato Criar(EquipamentoContrato equipamentoContrato);
        void Deletar(int codigo);
        void Atualizar(EquipamentoContrato equipamentoContrato);
        EquipamentoContrato ObterPorCodigo(int codigo);
        MtbfEquipamento CalcularMTBF(int codEquipContrato, DateTime? dataInicio, DateTime dataFim);
    }
}
