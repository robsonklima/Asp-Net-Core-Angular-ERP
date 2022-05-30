using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private Importacao AtualizacaoInstalacao(Importacao importacao)
        {
            var instList = new List<object>();

            importacao.ImportacaoLinhas
                        .Where(line => 
                                string.IsNullOrEmpty(
                                            line.ImportacaoColuna
                                                    .FirstOrDefault(col => col.Campo.Equals("CodInstalacao")).Valor))
                        .ToList()
                        .ForEach(line =>
                        {
                            var inst = new Instalacao();

                            Instalacao instObj = default;

                            line.ImportacaoColuna
                                    .ForEach(col =>
                                    {
                                        if (string.IsNullOrEmpty(col.Valor)) return;

                                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                                        var prop = inst.GetType().GetProperty(col.Campo);

                                        if (prop == null)
                                        {
                                            string saida;
                                            Constants.CONVERSOR_IMPORTACAO_INSTALACAO.TryGetValue(col.Campo, out saida);
                                            prop = inst.GetType().GetProperty(saida);
                                            var value = ConverterValor(col, instObj);
                                            prop.SetValue(inst, value);

                                        }
                                        else
                                        {
                                            dynamic value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);
                                            prop.SetValue(inst, value);

                                            if (col.Campo.Equals("CodInstalacao"))
                                                instObj = _instalacaoRepo.ObterPorCodigo((int)value);
                                        }
                                    });
                            instList.Add(inst);
                        });


            return new Importacao();
        }

        private dynamic ConverterValor(ImportacaoColuna coluna, Instalacao instObj)
        {
            switch (coluna.Campo)
            {
                case "NumSerie":
                    var equip = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{instObj.CodCliente}" });
                    return equip.FirstOrDefault().CodEquipContrato;
                case "NfVenda":
                    //var equip = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{instObj.CodCliente}" });
                    return 0;
                case "NfVendaData":
                    //var equip = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{instObj.CodCliente}" });
                    return 0;
                default:
                    return null;
            }
        }
    }
}