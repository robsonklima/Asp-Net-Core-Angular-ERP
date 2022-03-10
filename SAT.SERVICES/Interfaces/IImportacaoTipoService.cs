using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IImportacaoTipoService
    {
        ImportacaoTipo Criar(ImportacaoTipo importacaoConf);
        ListViewModel ObterPorParametros(ImportacaoTipoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(ImportacaoTipo importacaoConf);
        ImportacaoTipo ObterPorCodigo(int codigo);
    }
}