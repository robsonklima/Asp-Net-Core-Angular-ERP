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
                        var prop = inst?.GetType()?.GetProperty(col.Campo);

                        if (prop == null)
                        {
                            string saida;
                            Constants.DICIONARIO_CAMPOS_PLANILHA.TryGetValue(col.Campo, out saida);
                            prop = inst?.GetType()?.GetProperty(saida);
                            dynamic value;
                            value = ConverterCamposInstalacao(col, inst);
                            prop?.SetValue(inst, value);
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
                        linha.Mensagem = $"Erro ao mapear o registro. Registro: {inst.CodInstalacao} Campo: {col.Campo} Mensagem: {ex.Message}";
                        linha.Erro = true;
                        Mensagem.Add(linha.Mensagem);
                    }
                }
                   
                try
                {
                    if (inst.CodInstalacao > 0)
                    {
                        inst.CodUsuarioManut = usuario.CodUsuario;
                        inst.DataHoraManut = DateTime.Now;
                        inst = _instalacaoRepo.Atualizar(inst);
                        linha.Mensagem = $"Registro atualizado com sucesso: {inst.CodInstalacao}";
                        Mensagem.Add(linha.Mensagem);
                    }
                    else 
                    {
                        inst.CodUsuarioCad = usuario.CodUsuario;
                        inst.DataHoraCad = DateTime.Now;
                        inst = _instalacaoService.Criar(inst);
                        linha.Mensagem = $"Registro criado com sucesso: {inst.CodInstalacao}";
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

        private dynamic ConverterCamposInstalacao(ImportacaoColuna coluna, Instalacao inst)
        {
            switch (coluna.Campo)
            {
                case "NumSerie":
                    return _equipamentoContratoRepo
                        .ObterPorParametros(new EquipamentoContratoParameters { NumSerie = coluna.Valor, CodClientes = $"{inst.CodCliente}" })
                        ?.FirstOrDefault()
                        ?.CodEquipContrato;
                case "NFVenda":
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
                case "NFVendaData":
                    var updateNFVenda = _instalacaoNFVendaRepo.ObterPorCodigo(inst.CodInstalNFVenda.Value);
                    updateNFVenda.DataNFVenda = DateTime.Parse(coluna.Valor);
                    _instalacaoNFVendaRepo.Atualizar(updateNFVenda);
                    return inst.CodInstalNFVenda;      
                case "NumAgenciaDC":
                    var agenciaDc = coluna.Valor.Split("/");
                    if (agenciaDc.Count() < 2)
                        return null;

                    string agencia = agenciaDc[0];
                    string dc = agenciaDc[1];

                    return _localAtendimentoRepo
                        .ObterPorParametros(new LocalAtendimentoParameters { CodClientes = $"{inst.CodCliente}", DCPosto = dc, NumAgencia = agencia  })
                        ?.FirstOrDefault()
                        ?.CodPosto;                            
                default:
                    return ConverterCamposEmComum(coluna);
            }
        }
    }
}