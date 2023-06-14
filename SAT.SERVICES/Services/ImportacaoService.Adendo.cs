using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                var equip = new EquipamentoContrato();

                try
                {
                    foreach (var col in linha.ImportacaoColuna)
                    {

                        if (string.IsNullOrEmpty(col.Valor))
                            continue;

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper().Trim());
                        var prop = equip?.GetType()?.GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = equip?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposAdendo(col, equip);
                            prop?.SetValue(equip, value);
                        }
                        else
                        {
                            dynamic value;

                            if (col.Campo.Equals("CodEquipContrato")) 
                            {
                                value = Int32.Parse(col.Valor);
                                equip = _equipamentoContratoRepo.ObterPorCodigo(value);

                                if (equip is null)
                                    throw new Exception("Equipamento não encontrado");
                            }

                            if (col.Campo.Contains("IndSEMAT"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndSemat = value;
                            }

                            if (col.Campo.Contains("PontoEstrategico"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.PontoEstrategico = value;
                            }

                            if (col.Campo.Contains("IndPAE"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndPAE = value;
                            }

                            if (col.Campo.Contains("IndRHorario"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRHorario = value;
                            }

                            if (col.Campo.Contains("IndGarantia"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndGarantia = value;
                            }

                            if (col.Campo.Contains("IndReceita"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndReceita = value;
                            }

                            if (col.Campo.Contains("IndRepasse"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRepasse = value;
                            }

                            if (col.Campo.Contains("indRHorario"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRHorario = value;
                            }

                            if (col.Campo.Contains("IndInstalacao"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndInstalacao = value;
                            }

                            if (col.Campo.Equals("Horas_Racesso"))
                            {
                                value = int.Parse(col.Valor);
                                equip.LocalAtendimento.Cidade.HorasRAcesso = value;
                                _cidadeRepo.Atualizar(equip.LocalAtendimento.Cidade);
                                continue;
                            }

                            if (col.Campo.Equals("DistanciaKmPAT_Res"))
                            {
                                value = decimal.Parse(col.Valor, new CultureInfo("en-US"));
                                equip.LocalAtendimento.DistanciaKmPatRes = value;
                                _localAtendimentoRepo.Atualizar(equip.LocalAtendimento);
                                continue;
                            }

                            value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);
                            prop.SetValue(equip, value);
                        }
                    }

                    _equipamentoContratoRepo.Atualizar(equip);
                }
                catch
                {
                    
                }
            }

            return importacao;
        }

        private dynamic ConverterCamposAdendo(ImportacaoColuna coluna, EquipamentoContrato equip)
        {
            switch (coluna.Campo)
            {
                case "NumAgenciaDC":
                    var agenciaDc = coluna.Valor.Split("/");
                    if (agenciaDc.Count() < 2)
                        return null;

                    string agencia = agenciaDc[0];
                    string dc = agenciaDc[1];

                    return _localAtendimentoRepo
                        .ObterPorParametros(new LocalAtendimentoParameters { CodClientes = $"{equip.CodCliente}", DCPosto = dc, NumAgencia = agencia })
                        ?.FirstOrDefault()
                        ?.CodPosto;

                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}