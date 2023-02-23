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
        private Importacao ImportacaoInstalacao(Importacao importacao)
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

                        col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                        var prop = inst.GetType().GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo.ToLower(), out saida);
                            prop = inst.GetType().GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposEmComum(col) || ConverterCamposInstalacao(col, inst);
                            prop.SetValue(inst, value);
                        }
                        else
                        {
                            dynamic value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, prop.PropertyType);
                            prop.SetValue(inst, value);

                            if (col.Campo.Equals("CodInstalacao"))
                                inst = _instalacaoRepo.ObterPorCodigo((int)value);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        linha.Mensagem = $"Erro ao mapear as Instalação. Instalação: {inst.CodInstalacao} Campo: {col.Campo} Mensagem: {ex.Message}";
                        linha.Erro = true;
                        Mensagem.Add(linha.Mensagem);
                    }
                }
                   
                try
                {
                    inst.CodUsuarioManut = usuario.CodUsuario;
                    inst.DataHoraManut = DateTime.Now;

                    if (inst.CodInstalacao > 0)
                    {
                        inst = _instalacaoRepo.Atualizar(inst);
                        linha.Mensagem = $"Instalação atualizada com sucesso: {inst.CodInstalacao}";
                        Mensagem.Add(linha.Mensagem);
                    }
                    else 
                    {
                        inst = _instalacaoRepo.Criar(inst);
                        linha.Mensagem = $"Instalação criada com sucesso: {inst.CodInstalacao}";
                        Mensagem.Add(linha.Mensagem);
                    }
                }
                catch (System.Exception ex)
                {
                    linha.Mensagem = $"Erro ao montar Instalação! Mensagem: {ex.Message}";
                    linha.Erro = true;
                    Mensagem.Add(linha.Mensagem);
                }
            }

            string[] destinatarios = { usuario.Email };

            var email = new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = "Atualização/Importação em massa de instalações",
                Corpo = String.Join("<br>", Mensagem),
            };

            _emailService.Enviar(email);

            return importacao;
        }

        private dynamic ConverterCamposInstalacao(ImportacaoColuna coluna, Instalacao inst)
        {
            switch (coluna.Campo)
            {
                case "num_serie":
                    return _equipamentoContratoRepo
                        .ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{inst.CodCliente}" })
                        ?.FirstOrDefault()
                        ?.CodEquipContrato;
                case "nf_venda":
                    var instalNFVenda = _instalacaoNFVendaRepo.ObterPorParametros(new InstalacaoNFVendaParameters { NumNFVenda = Int32.Parse(coluna.Valor) })?.FirstOrDefault();
                    if (instalNFVenda == null)
                    {
                        instalNFVenda = _instalacaoNFVendaRepo.Criar(new InstalacaoNFVenda
                        {
                            CodCliente = inst.CodCliente,
                            NumNFVenda = Int32.Parse(coluna.Valor),
                            CodUsuarioCad = _contextAcecssor.HttpContext.User.Identity.Name,
                            DataHoraCad = DateTime.Now
                        });
                    }
                    return instalNFVenda.CodInstalNFvenda;
                case "nf_venda_data":
                    var updateNFVenda = _instalacaoNFVendaRepo.ObterPorCodigo(inst.CodInstalNFVenda.Value);
                    updateNFVenda.DataNFVenda = DateTime.Parse(coluna.Valor);
                    _instalacaoNFVendaRepo.Atualizar(updateNFVenda);
                    return inst.CodInstalNFVenda;
                default:
                    return null;
            }
        }
    }
}