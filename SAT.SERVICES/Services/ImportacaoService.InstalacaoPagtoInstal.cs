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
        private Importacao ImportacaoInstalacaoPagtoInstal(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            List<string> Mensagem = new List<string>();

            foreach (var linha in importacao.ImportacaoLinhas)
            {
                var inst = new InstalacaoPagtoInstal();

                foreach (var col in linha.ImportacaoColuna)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(col.Valor))
                            continue;

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                        var prop = inst?.GetType()?.GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = inst?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposInstalacaoPagtoInstal(col, inst);
                            prop?.SetValue(inst, value);
                        }
                        else
                        {
                            dynamic value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);
                            prop.SetValue(inst, value);


                            if (col.Campo.Equals("VlrMulta"))
                            {
                                int codInstalacao = Int32.Parse(linha.ImportacaoColuna[0].Valor);
                                int codInstalPagto = Int32.Parse(linha.ImportacaoColuna[0].Valor);
                                int codInstalTipoParcela = Int32.Parse(linha.ImportacaoColuna[0].Valor);

                                inst = _instalacaoPagtoInstalRepo.ObterPorCodigo(codInstalacao, codInstalPagto, codInstalTipoParcela);
                            }

                        }
                    }
                    catch (System.Exception ex)
                    {
                        linha.Mensagem = $"Erro ao mapear o registro. Registro: {inst.CodInstalacao} Campo: {col.Campo} Mensagem: {ex.Message}";
                        linha.Erro = true;
                        Mensagem.Add(linha.Mensagem);
                    }
                }

                try
                {
                    inst.CodUsuarioCad = usuario.CodUsuario;
                    inst.DataHoraCad = DateTime.Now;
                    inst = _instalacaoPagtoInstalService.Criar(inst);
                    linha.Mensagem = $"Registro criado com sucesso: {inst.CodInstalacao}";
                    Mensagem.Add(linha.Mensagem);
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

        private dynamic ConverterCamposInstalacaoPagtoInstal(ImportacaoColuna coluna, InstalacaoPagtoInstal inst)
        {
            switch (coluna.Campo)
            {
                case "NumSerie":
                    return _equipamentoContratoRepo
                        .ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{inst.Instalacao.CodCliente}" })
                        ?.FirstOrDefault()
                        ?.CodEquipContrato;          
                case "NroContrato":
                    return _contratoRepo
                        .ObterPorParametros(new ContratoParameters { NroContrato = coluna.Valor, CodClientes = $"{inst.Instalacao.CodCliente}" })
                        ?.FirstOrDefault()
                        ?.CodContrato;                                       
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}