using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        private Importacao ImportacaoProcessoReparo(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            List<string> Mensagem = new List<string>();

            foreach (var linha in importacao.ImportacaoLinhas)
            {
                var ori = new ORItem();

                foreach (var col in linha.ImportacaoColuna)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(col.Valor)) 
                            continue;

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper().Trim());
                        var prop = ori?.GetType()?.GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = ori?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposProcessoReparo(col, ori);
                            prop?.SetValue(ori, value);
                        }
                        else
                        {
                            dynamic value;

                            if (col.Campo.Equals("CodORItem"))
                                value = int.Parse(col.Valor);                                                                                                                                                                               
                            else
                                value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);

                            prop.SetValue(ori, value);

                            if (col.Campo.Equals("CodORItem"))
                                ori = _orItemRepo.ObterPorCodigo((int)value);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        switch(ex.Message)
                        {
                            case var s when ex.Message.Contains("Input string was not in a correct format."):
                                linha.Mensagem = $"Foi informada um dado inválido para o campo {col.Campo}";
                                linha.Erro = true;
                                Mensagem.Add(linha.Mensagem);
                                break;
                            case var s when ex.Message.Contains("was not recognized as a valid DateTime."):
                                linha.Mensagem = $"Formato de data inválido no campo {col.Campo}";
                                linha.Erro = true;
                                Mensagem.Add(linha.Mensagem);
                                break;
                            default:
                                linha.Mensagem = $"Erro ao importar registro! Mensagem: {ex.Message}";
                                linha.Erro = true;
                                Mensagem.Add(linha.Mensagem);
                                break;                            
                        }     
                    }
                }
                   
                try
                {
                    if (ori.CodORItem > 0)
                    {
                        ori = _orItemRepo.Atualizar(ori);
                        linha.Mensagem = $"Registro atualizado com sucesso: {ori.CodORItem}";
                        Mensagem.Add(linha.Mensagem);
                    }
                }                    
                catch (System.Exception ex)
                {                    
                    switch(ex.Message)
                    {
                        case var s when ex.Message.Contains("is part of a key and so cannot be modified or marked as modified"):
                            linha.Mensagem = $"Campos obrigatórios da linha não podem ser modificados: {ex.Message}";
                            linha.Erro = true;
                            Mensagem.Add(linha.Mensagem);
                            break;
                        default:
                            linha.Mensagem = $"Erro ao importar linhas! {ex.Message}";
                            linha.Erro = true;
                            Mensagem.Add(linha.Mensagem);
                            break;                            
                    }                    
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

        private dynamic ConverterCamposProcessoReparo(ImportacaoColuna coluna, ORItem ori)
        {
            switch (coluna.Campo)
            {
                case "MagnusPeca":
                    return _pecaRepo.ObterPorParametros(new PecaParameters { CodMagnus = coluna.Valor })?.FirstOrDefault()?.CodPeca;                           
                case "ORStatus":
                    return _orStatusRepo.ObterPorParametros(new ORStatusParameters { Abrev = coluna.Valor })?.FirstOrDefault()?.CodStatus;                                               
                case "NomeUsuarioLab":
                    return _usuarioRepo.ObterPorParametros(new UsuarioParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodUsuario; 
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}