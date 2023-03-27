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
        private Importacao ImportacaoEquipamentoContrato(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            List<string> Mensagem = new List<string>();

            foreach (var linha in importacao.ImportacaoLinhas)
            {
                var equipamento = new EquipamentoContrato();

                foreach (var col in linha.ImportacaoColuna)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(col.Valor)) 
                            continue;

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                        var prop = equipamento?.GetType()?.GetProperty(col.Campo);

                        if (prop == null) 
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = equipamento?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposEquipamentoContrato(col, equipamento);
                            prop?.SetValue(equipamento, value);
                        }
                        else
                        {    
                            dynamic value;

                            if(col.Campo.Equals("IndGarantia") || col.Campo.Equals("IndReceita") || col.Campo.Equals("IndRepasse") || col.Campo.Equals("IndInstalacao"))
                                value = byte.Parse(col.Valor);
                            else 
                                value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);

                            prop.SetValue(equipamento, value);

                            if (col.Campo.Equals("CodEquipContrato"))
                                equipamento = _equipamentoContratoRepo.ObterPorCodigo((int)value);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        linha.Mensagem = $"Erro ao mapear o registro. Registro: {equipamento.CodEquipContrato} Campo: {col.Campo} Mensagem: {ex.Message}";
                        linha.Erro = true;
                        Mensagem.Add(linha.Mensagem);
                    }
                }
                   
                try
                {
                    if (equipamento.CodEquipContrato > 0)
                    {
                        equipamento.CodUsuarioManut = usuario.CodUsuario;
                        equipamento.DataHoraManut = DateTime.Now;
                        equipamento = _equipamentoContratoRepo.Atualizar(equipamento);
                        linha.Mensagem = $"Registro atualizado com sucesso: {equipamento.CodEquipContrato}";
                        Mensagem.Add(linha.Mensagem);
                    }
                    else 
                    {
                        equipamento.CodUsuarioCad = usuario.CodUsuario;
                        equipamento.DataHoraCad = DateTime.Now;
                        equipamento = _equipamentoContratoService.Criar(equipamento);
                        linha.Mensagem = $"Registro criado com sucesso: {equipamento.CodEquipContrato}";
                        Mensagem.Add(linha.Mensagem);
                    }
                }
                catch (System.Exception ex)
                {
                    linha.Mensagem = $"Erro ao montar registro! Mensagem: {ex.Message}";
                    linha.Erro = true;
                    Mensagem.Add(linha.Mensagem);
                }
            }

            string[] destinatarios = { usuario.Email };

            var email = new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = "SAT 2.0 - Importação",
                Corpo = String.Join("<br>", Mensagem),
            };

            _emailService.Enviar(email);

            return importacao;
        }

        private dynamic ConverterCamposEquipamentoContrato(ImportacaoColuna coluna, EquipamentoContrato equipamento)
        {
            switch (coluna.Campo)
            {
                case "NumAgenciaDC":
                    if (coluna.Valor.Count() < 8) 
                        return null;

                    return _localAtendimentoRepo.ObterPorParametros(new LocalAtendimentoParameters
                    {
                        IndAtivo = 1,
                        CodCliente = equipamento.CodCliente,
                        NumAgencia = coluna.Valor.Substring(0, 5),
                        DCPosto = coluna.Valor.Substring(6, 2)
                    })?.FirstOrDefault()?.CodPosto;
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}