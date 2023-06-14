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

                foreach (var col in linha.ImportacaoColuna)
                {
                    try
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

                            if(col.Campo.Equals("IndSEMAT") || col.Campo.Equals("PontoEstrategico") 
                                || col.Campo.Equals("indRHorario") || col.Campo.Equals("indPAE") 
                                || col.Campo.Equals("IndGarantia") || col.Campo.Equals("IndReceita") 
                                || col.Campo.Equals("IndRepasse") || col.Campo.Equals("IndInstalacao")
                                )
                                value = byte.Parse(col.Valor); 
                            // else if (col.Campo.Equals("Horas_Racesso"))   --INSERIR NA TABELA CIDADE
                            //     value = int.Parse(col.Valor); 
                            // else if (col.Campo.Equals("DistanciaKmPAT_Res")) -- INSERIR NA TABELA LOCALATENDIMENTO
                            //     value = decimal.Parse(col.Valor, new CultureInfo("en-US"));                                                                                                                                                                                    
                            else
                                value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);

                            prop.SetValue(equip, value);

                            if (col.Campo.Equals("CodEquipContrato"))
                                equip = _equipamentoContratoRepo.ObterPorCodigo((int)value);
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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
                        .ObterPorParametros(new LocalAtendimentoParameters { CodClientes = $"{equip.CodCliente}", DCPosto = dc, NumAgencia = agencia  })
                        ?.FirstOrDefault()
                        ?.CodPosto;
                        
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}

//Primeira ação EC
//codSLA,	IndSEMAT, CodContrato = 3145,PontoEstrategico, indRHorario, indrAcesso, indPAE, indAtivo = 1, indReceita = 1, IndInstalacao = 	1	
//Segunda ação EC
//codposto
//Terceira ação CIDADE
//Horas_Racesso  alterar esta coluna da tabela cidade com base na cidade do LA atendimento do EC
//Quarta ação LA
// DistanciaKmPAT_Res
//Quinta ação EC
//CodFilial alterar com base na tabela cidade vinculada ao EC
// Sexta ação EC
//CodAutorizada com base na cidade e filial iniciada por P
//Setima ação EC
//CodRegiao com base no Local Atendimento

//codequipcontrato,codSla,IndSEMAT,PontoEstrategico,indRHorario,indrAcesso,indPAE,codposto,Horas_Racesso,DistanciaKmPAT_Res