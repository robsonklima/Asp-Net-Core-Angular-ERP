using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private Importacao ImportacaoOrdemServico(Importacao importacao)
        {
            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            var reservaContador = _sequenciaRepo.AtualizaContadorOS(importacao.ImportacaoLinhas.Count()) + 1;
            
            List<string> Mensagem = new List<string>();

            importacao.ImportacaoLinhas
                .Where(line => !string.IsNullOrEmpty(line.ImportacaoColuna
                    .FirstOrDefault(col => col.Campo.Equals("codEquipContrato")).Valor))
                .ToList()
                .ForEach(line =>
                {
                    var os = new OrdemServico();
                    line.ImportacaoColuna
                            .ForEach(col =>
                            {
                                try
                                {
                                    if (string.IsNullOrEmpty(col.Valor)) return;

                                    col.Campo = Regex.Replace(col.Campo, "^[a-z]", m => m.Value.ToUpper());
                                    var prop = os.GetType().GetProperty(col.Campo);
                                    dynamic value = prop.PropertyType == typeof(DateTime?) ? DateTime.Parse(col.Valor) : Convert.ChangeType(col.Valor, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                                    prop.SetValue(os, value);
                                }
                                catch (System.Exception ex)
                                {
                                    line.Mensagem = $"Erro ao mapear as OS. Equipamento: {os.CodEquipContrato} Campo: {col.Campo} Mensagem: {ex.Message}";
                                    line.Erro = true;
                                    Mensagem.Add(line.Mensagem);
                                    return;
                                }
                            });

                    try
                    {
                        var equip = _equipamentoContratoRepo.ObterPorCodigo(os.CodEquipContrato.Value);

                        os.CodOS = reservaContador;
                        reservaContador++;

                        os.CodStatusServico = Constants.STATUS_SERVICO_ABERTO;
                        os.IndStatusEnvioReincidencia = 1;
                        os.CodCliente = equip.CodCliente;
                        os.CodFilial = equip.CodFilial;
                        os.CodAutorizada = equip.CodAutorizada;
                        os.CodRegiao = equip.CodRegiao;
                        os.CodPosto = equip.CodPosto;
                        os.CodEquip = equip.CodEquip;
                        os.CodTipoEquip = equip.CodTipoEquip;
                        os.CodGrupoEquip = equip.CodGrupoEquip;
                        os.CodUsuarioCad = usuario.CodUsuario;
                        os.DataHoraCad = DateTime.Now;
                        os.DataHoraSolicitacao = os.DataHoraCad;
                        os.DataHoraAberturaOS = os.DataHoraCad;

                        _ordemServicoRepo.Criar(os);

                        Mensagem.Add($"OS Criada com Sucesso: {os.CodOS}");
                    }
                    catch (System.Exception ex)
                    {
                        line.Mensagem = $"Erro ao montar OS! Equipamento:{os.CodEquipContrato}. Mensagem: {ex.Message}";
                        line.Erro = true;
                        Mensagem.Add(line.Mensagem);
                    }
                });

            string[] destinatarios = { usuario.Email };

            var email = new Email
            {
                EmailDestinatarios = destinatarios,
                Assunto = "Abertura em massa de chamados",
                Corpo = String.Join("<br>", Mensagem),
            };

            _emailService.Enviar(email);

            return importacao;
        }
    }
}
