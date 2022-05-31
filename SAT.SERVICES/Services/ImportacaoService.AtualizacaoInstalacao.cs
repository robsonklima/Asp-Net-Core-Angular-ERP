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
        public List<string> Mensagem = new List<string>();


        private Importacao AtualizacaoInstalacao(Importacao importacao)
        {
            var instList = new List<Instalacao>();

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
                                            Mensagem.Add($"Erro ao mapear as colunas. Código: {inst.CodInstalacao} Campo: {col.Campo}");
                                        }
                                    });
                            instList.Add(inst);
                        });

            instList.ForEach(inst =>
            {
                try
                {
                    _instalacaoRepo.Atualizar(inst);
                    Mensagem.Add($"Implantação: {inst.CodInstalacao} atualizado com sucesso.");
                }
                catch (System.Exception ex)
                {
                    Mensagem.Add($"Erro ao atualizar! Código:{inst.CodInstalacao}. Mensagem: {ex.Message}");
                }
            });

            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            var email = new Email
            {
                EmailRemetente = "equipe.sat@perto.com.br",
                NomeRemetente = "Sistema SAT",
                EmailCC = "equipe.sat@perto.com.br",
                NomeCC = "Equipe SAT",
                EmailDestinatario = usuario.Email,
                NomeDestinatario = usuario.NomeUsuario,
                Assunto = "Atualização em massa do módulo Implantação",
                Corpo = String.Join("<br>", Mensagem),
            };

            _emailService.Enviar(email);

            return new Importacao();
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
                    return 0;
            }
        }
    }
}