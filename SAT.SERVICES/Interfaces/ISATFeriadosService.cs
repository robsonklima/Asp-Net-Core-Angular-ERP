using System;
using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ISATFeriadosService
    {
        ListViewModel ObterPorParametros(SATFeriadosParameters parameters);
        SATFeriados Criar(SATFeriados satSATFeriadoss);
        void Deletar(int codigo);
        void Atualizar(SATFeriados satSATFeriadoss);
        SATFeriados ObterPorCodigo(int codigo);
    }
}
