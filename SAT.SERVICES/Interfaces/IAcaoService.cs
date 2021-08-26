﻿using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAcaoService
    {
        ListViewModel ObterPorParametros(AcaoParameters parameters);
        Acao Criar(Acao acao);
        void Deletar(int codigo);
        void Atualizar(Acao acao);
        Acao ObterPorCodigo(int codigo);
    }
}
