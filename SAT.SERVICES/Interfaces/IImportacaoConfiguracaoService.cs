using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IImportacaoConfiguracaoService
    {
        ImportacaoConfiguracao Criar(ImportacaoConfiguracao importacaoConf);
        ListViewModel ObterPorParametros(ImportacaoConfiguracaoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(ImportacaoConfiguracao importacaoConf);
        ImportacaoConfiguracao ObterPorCodigo(int codigo);
    }
}