using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private Importacao AtualizacaoInstalacao(Importacao importacao)
        {
            var instList = new List<object>();

            importacao.ImportacaoLinhas
                        .ForEach(line =>
                        {
                            var inst = new Instalacao();
                            line.ImportacaoColuna
                                    .ForEach(col =>
                                    {
                                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                                        var prop = inst.GetType().GetProperty(col.Campo);
                                        var value = Convert.ChangeType(col.Valor, prop.PropertyType);
                                        prop.SetValue(inst, value);
                                    });
                            instList.Add(inst);
                        });


            return new Importacao();
        }
    }
}