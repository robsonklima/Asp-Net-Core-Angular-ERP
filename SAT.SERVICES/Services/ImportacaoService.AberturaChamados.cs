using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private List<string> AberturaChamadosEmMassa(List<ImportacaoAberturaOrdemServico> importacaoOs)
        {
            var osMensagem = new List<string>();

            importacaoOs.Where(o => o.CodEquipContrato is not null)
                .ToList()
                .ForEach(i =>
                {

                    var equipamento = _equipamentoContratoRepo.ObterPorCodigo(i.CodEquipContrato.Value);

                    if (equipamento is null) osMensagem.Add($"Equipamento não encontrado - ID: {i.CodEquipContrato}");

                    if (equipamento is not null)
                    {
                        try
                        {
                            int codOs = _sequenciaRepo.ObterContador("OS");

                            var os = new OrdemServico
                            {
                                CodOS = codOs,
                                CodCliente = equipamento.CodCliente,
                                CodPosto = equipamento.LocalAtendimento.CodPosto.Value,
                                CodTipoIntervencao = int.Parse(i.CodTipoIntervencao),
                                CodFilial = equipamento.CodFilial,
                                CodRegiao = equipamento.CodRegiao,
                                CodAutorizada = equipamento.CodAutorizada,
                                CodEquipContrato = equipamento.CodEquipContrato,
                                CodEquip = equipamento.CodEquip,
                                CodUsuarioCad = _contextAcecssor.HttpContext.User.Identity.Name,
                                DefeitoRelatado = i.DefeitoRelatado,
                                NumOSCliente = i.NumOSCliente,
                                NumOSQuarteirizada = i.NumOSQuarteirizada,
                                DataHoraCad = DateTime.Now,
                                DataHoraSolicitacao = DateTime.Now,
                                DataHoraAberturaOS = DateTime.Now,
                                IndStatusEnvioReincidencia = -1,
                                IndRevisaoReincidencia = 1,
                                CodStatusServico = 1
                            };

                            _ordemServicoRepo.Criar(os);

                            osMensagem.Add($"Chamado criado - {codOs} - Série: {equipamento.NumSerie}");
                        }
                        catch (Exception ex)
                        {
                            osMensagem.Add($"Erro ao criar (exceção gerada) - Série: {equipamento.NumSerie} ID: {equipamento.CodEquipContrato}");
                        }
                    }
                });

            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            _emailService.Enviar(new Email
            {
                NomeRemetente = "SAT",
                EmailRemetente = "equipe.sat@perto.com.br",
                NomeDestinatario = usuario.NomeUsuario,
                EmailDestinatario = usuario.Email,
                NomeCC = "SAT",
                EmailCC = "equipe.sat@perto.com.br",
                Assunto = "Abertura de chamados em massa",
                Corpo = GerarHTML(osMensagem)
            });

            return osMensagem;
        }

        private static string GerarHTML<T>(List<T> list, params string[] columns)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append($"{item}<br>");
            }

            sb.Append(Constants.ASSINATURA_EMAIL);
            return sb.ToString();
        }
    }
}