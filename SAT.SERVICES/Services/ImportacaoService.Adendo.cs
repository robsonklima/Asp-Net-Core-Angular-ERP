using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private Importacao ImportacaoAdendo(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            List<string> Mensagem = new List<string>();

            foreach (var linha in importacao.ImportacaoLinhas)
            {
                var inst = new Instalacao();

                foreach (var col in linha.ImportacaoColuna)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(col.Valor)) 
                            continue;

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper().Trim());
                        var prop = inst?.GetType()?.GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = inst?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposAdendo(col, inst);
                            prop?.SetValue(inst, value);
                        }
                        else
                        {
                            
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return importacao;
        }

        private dynamic ConverterCamposAdendo(ImportacaoColuna coluna, Instalacao inst)
        {
            switch (coluna.Campo)
            {
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}