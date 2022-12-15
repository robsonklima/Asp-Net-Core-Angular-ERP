﻿using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IImprodutividadeService
    {
        ListViewModel ObterPorParametros(ImprodutividadeParameters parameters);
        Improdutividade ObterPorCodigo(int codigo);
    }
}
