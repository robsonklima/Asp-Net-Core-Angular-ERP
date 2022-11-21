using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        public List<string> Mensagem = new List<string>();
        private Importacao AtualizacaoInstalacao(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            importacao.ImportacaoLinhas
                        .Where(line =>
                                !string.IsNullOrEmpty(
                                            line.ImportacaoColuna
                                                    .FirstOrDefault(col => col.Campo.Equals("codInstalacao")).Valor))
                        .ToList()
                        .ForEach(line =>
                        {
                            var inst = new Instalacao();
                            line.ImportacaoColuna
                                    .ForEach(col =>
                                    {
                                        try
                                        {
                                            if (string.IsNullOrEmpty(col.Valor)) return;

                                            col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                                            var prop = inst.GetType().GetProperty(col.Campo);

                                            if (prop == null)
                                            {
                                                string saida;
                                                Constants.CONVERSOR_IMPORTACAO_INSTALACAO.TryGetValue(col.Campo, out saida);
                                                prop = inst.GetType().GetProperty(saida);
                                                var value = ConverterValor(col, inst);
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
                                            line.Mensagem = $"Erro ao mapear as colunas. Código: {inst.CodInstalacao} Campo: {col.Campo} Mensagem: {ex.Message}";
                                            line.Erro = true;

                                            return;
                                        }
                                    });
                            try
                            {
                                inst.CodUsuarioManut = usuario.CodUsuario;
                                inst.DataHoraManut = DateTime.Now;

                                _instalacaoRepo.Atualizar(inst);

                                line.Mensagem = $"Implantação: {inst.CodInstalacao} atualizado com sucesso.";
                                line.Erro = false;
                                Mensagem.Add(line.Mensagem);

                            }
                            catch (System.Exception ex)
                            {
                                line.Mensagem = $"Erro ao atualizar! Código:{inst.CodInstalacao}. Mensagem: {ex.Message}";
                                line.Erro = true;
                                Mensagem.Add(line.Mensagem);
                            }
                        });

            string[] destinatarios = { usuario.Email };

            var email = new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = "Atualização em massa do módulo Implantação",
                Corpo = String.Join("<br>", Mensagem),
            };

            _emailService.Enviar(email);

            return importacao;
        }

        private dynamic ConverterValor(ImportacaoColuna coluna, Instalacao inst)
        {
            switch (coluna.Campo)
            {
                case "NumSerie":
                    var equip = _equipamentoContratoRepo.ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{inst.CodCliente}" });
                    return equip.FirstOrDefault().CodEquipContrato;
                case "NfVenda":
                    var instalNFVenda = _instalacaoNFVendaRepo.ObterPorParametros(new InstalacaoNFVendaParameters { NumNFVenda = Int32.Parse(coluna.Valor) }).FirstOrDefault();
                    if (instalNFVenda == null)
                    {
                        instalNFVenda = _instalacaoNFVendaRepo
                                            .Criar(new InstalacaoNFVenda
                                            {
                                                CodCliente = inst.CodCliente,
                                                NumNFVenda = Int32.Parse(coluna.Valor),
                                                CodUsuarioCad = _contextAcecssor.HttpContext.User.Identity.Name,
                                                DataHoraCad = DateTime.Now
                                            });
                    }
                    return instalNFVenda.CodInstalNfvenda;
                case "NfVendaData":
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