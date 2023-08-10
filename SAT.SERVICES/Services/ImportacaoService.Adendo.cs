using NLog;
using NLog.Fluent;
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
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
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

                                if (equip is null) {
                                    linha.Mensagem = $"Equipamento não encontrado";
                                    linha.Erro = true;
                                    Mensagem.Add(linha.Mensagem);
                                    continue;
                                }
                            }

                            if (col.Campo.Contains("IndSemat"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndSemat = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndPAE"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndPAE = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndRAcesso"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRHorario = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndReceita"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndReceita = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndAtivo"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndReceita = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndRepasse"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRepasse = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndRHorario"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndRHorario = value;
                                continue;
                            }

                            if (col.Campo.Contains("IndInstalacao"))
                            {
                                value = byte.Parse(col.Valor);
                                equip.IndInstalacao = value;
                                continue;
                            }

                            if (col.Campo.Contains("Horas_RAcesso"))
                            {
                                value = int.Parse(col.Valor);
                                var cidade = equip?.LocalAtendimento?.Cidade!;
                                
                                if(cidade is not null)
                                {
                                    cidade.Horas_RAcesso = value!;
                                    _cidadeRepo.Atualizar(cidade);
                                }
                                
                                continue;
                            }

                            if (col.Campo.Contains("DistanciaKmPAT_Res"))
                            {
                                value = decimal.Parse(col.Valor, new CultureInfo("en-US"));
                                equip.LocalAtendimento.DistanciaKmPatRes = value;
                                equip.CodFilial = equip.LocalAtendimento.Cidade.CodFilial;
                                equip.CodAutorizada = equip.Autorizada.CodFilial;
                                equip.CodRegiao = equip.Autorizada.CodFilial;
                                _localAtendimentoRepo.Atualizar(equip.LocalAtendimento);
                                continue;
                            }

                            value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);
                            prop.SetValue(equip, value);
                        }
                    }

                    equip.DataHoraManut = DateTime.Now;
                    equip.CodUsuarioManut = usuario.CodUsuario;
                    equip.CodUsuarioManutencao = usuario.CodUsuario;
                    _equipamentoContratoRepo.Atualizar(equip);

                    _logger.Info()
                        .Message($"ADENDO: Atualizando parque, equipamento: { equip.CodEquipContrato }")
                        .Property("application", Constants.SISTEMA_CAMADA_API)
                        .Write();
                    
                    //DesativarParqueNaoInformado();
                }
                catch (Exception ex)
                {
                    linha.Mensagem = $"{ex.Message}";
                    linha.Erro = true;
                    Mensagem.Add(linha.Mensagem);

                    _logger.Error()
                        .Message($"ADENDO: Erro ao atualizar parque, equipamento: { equip.CodEquipContrato }")
                        .Property("application", Constants.SISTEMA_CAMADA_API)
                        .Write();
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
    
        private void DesativarParqueNaoInformado() {
            try
            {
                var equipamentos = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters {
                    CodClientes = Constants.CLIENTE_BB.ToString(),
                    CodContrato = Constants.CONTRATO_BB_TECNOLOGIA,
                    DataHoraManutInicio = DateTime.Now.AddYears(-5),
                    DataHoraManutFim = DateTime.Now.AddHours(-1),
                    IndAtivo = 1
                });

                foreach (var equip in equipamentos)
                {
                    equip.IndAtivo = 0;
                    equip.IndReceita = 0;
                    equip.IndRepasse = 0;
                    equip.IndGarantia = 0;
                    equip.IndInstalacao = 0;
                    equip.DataDesativacao = DateTime.Now;
                    equip.DataHoraManut = DateTime.Now;
                    equip.CodUsuarioManut = "SAT";
                    equip.CodUsuarioManutencao = "SAT";

                    _equipamentoContratoRepo.Atualizar(equip);
                }

                _logger.Info()
                    .Message($"ADENDO: Desativando { equipamentos.Count() } equipamentos")
                    .Property("application", Constants.SISTEMA_CAMADA_API)
                    .Write();
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message("ADENDO: Erro ao desativar parque: ", ex.Message)
                    .Property("application", Constants.SISTEMA_CAMADA_API)
                    .Write();
            }
        }
    }
}