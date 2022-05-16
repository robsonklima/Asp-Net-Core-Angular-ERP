using System.Linq;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOrdemServico(OrdemServicoParameters parameters)
        {
            var os = _osRepo.ObterPorParametros(parameters);
            var osSheet = os.Select(os =>
                             new
                             {
                                 Chamado = os.CodOS,
                                 NumOSCliente = os.NumOSCliente,
                                 DataAbertura = os.DataHoraAberturaOS?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 DataSolicitacao = os.DataHoraSolicitacao?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 LimiteAtendimento = os.PrazosAtendimento?.OrderByDescending(i => i.CodOSPrazoAtendimento)?.FirstOrDefault()?.DataHoraLimiteAtendimento?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 Status = os.StatusServico?.NomeStatusServico?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Intervencao = os.TipoIntervencao?.CodETipoIntervencao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Tecnico = os.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                                 NumBanco = os.Cliente?.NumBanco ?? Constants.NENHUM_REGISTRO,
                                 NumAgencia = os.LocalAtendimento != null ? ($"{os.LocalAtendimento.NumAgencia}/{os.LocalAtendimento.DCPosto}") : Constants.NENHUM_REGISTRO,
                                 Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Cliente = os.EquipamentoContrato?.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                 Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                 Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                 Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                 Regiao = os.Regiao?.NomeRegiao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 PA = os.RegiaoAutorizada?.PA ?? 0,
                                 Autorizada = os.Autorizada?.NomeFantasia?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Filial = os.Filial?.NomeFilial,
                                 SLA = os.EquipamentoContrato?.AcordoNivelServico?.NomeSLA ?? Constants.NENHUM_REGISTRO,
                                 Reincidencia = os.NumReincidencia ?? 0,
                                 Defeito = os.DefeitoRelatado ?? Constants.NENHUM_REGISTRO,
                                 Fechamento = os.DataHoraFechamento,
                             });

            var ratSheet = os.SelectMany(os => os.RelatoriosAtendimento.Select(r =>
                             new
                             {
                                 Chamado = r.CodOS,
                                 CodRat = r.CodRAT,
                                 NumRat = r.NumRAT ?? Constants.NENHUM_REGISTRO,
                                 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Tecnico = r.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                                 Status = r.StatusServico?.NomeStatusServico ?? Constants.NENHUM_REGISTRO,
                                 DataInicio = r.DataHoraInicio.Date.ToString() ?? Constants.NENHUM_REGISTRO,
                                 DataSolucao = r.DataHoraSolucao.Date.ToString() ?? Constants.NENHUM_REGISTRO,
                                 Hora = r.DataHoraSolucao.ToString("HH:mm"),
                                 Cliente = os.EquipamentoContrato?.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                 Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                 Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                 Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                 TipoServico = r.TipoServico?.NomeServico ?? Constants.NENHUM_REGISTRO,
                                 Observacao = r.ObsRAT ?? Constants.NENHUM_REGISTRO,
                                 RelatoSolucao = r.RelatoSolucao ?? Constants.NENHUM_REGISTRO,
                                 DataCadastro = r.DataHoraCad,
                             }
                        ));

            var ratDetalheSheet = os.SelectMany(os => os.RelatoriosAtendimento
                                    .SelectMany(rat => rat.RelatorioAtendimentoDetalhes
                                    .Select(d =>
                                        new
                                        {
                                            Chamado = rat?.CodOS,
                                            NumRat = rat?.CodRAT,
                                            Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                            Cliente = os.EquipamentoContrato?.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                            Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                            CodTipoCausa = d.TipoCausa?.CodETipoCausa ?? Constants.NENHUM_REGISTRO,
                                            TipoCausa = d.TipoCausa?.NomeTipoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodGrupoCausa = d.GrupoCausa?.CodEGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            GrupoCausa = d.GrupoCausa?.NomeGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodDefeito = d.Defeito?.CodEDefeito ?? Constants.NENHUM_REGISTRO,
                                            Defeito = d.Defeito?.NomeDefeito ?? Constants.NENHUM_REGISTRO,
                                            CodCausa = d.Causa?.CodECausa ?? Constants.NENHUM_REGISTRO,
                                            Causa = d.Causa?.NomeCausa ?? Constants.NENHUM_REGISTRO,
                                            CodAcao = d.Acao?.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                            Acao = d.Acao?.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                            OrigemCausa = d.CodOrigemCausa != null ? d.CodOrigemCausa.Value == 1 ? "Máquina" : "Extra Máquina" : "Causa Origem Não Informada"
                                        }
                                    )));

            var ratDetalhePecaSheet = os.SelectMany(os => os.RelatoriosAtendimento
                                        .SelectMany(rat => rat.RelatorioAtendimentoDetalhes
                                        .SelectMany(rp => rp.RelatorioAtendimentoDetalhePecas
                                        .Select(p =>
                                        new
                                        {
                                            Chamado = rat.CodOS,
                                            NumRat = rat.NumRAT ?? Constants.NENHUM_REGISTRO,
                                            Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Regiao = os.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO,
                                            Filial = os.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                            Cliente = os.EquipamentoContrato?.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                            Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                            // ProtocoloSTN = rat.ProtocolosSTN != null ? "CU" : Constants.NENHUM_REGISTRO ,
                                            CodAcao = rp.Acao?.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                            Acao = rp.Acao?.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                            CodMagnus = p.Peca.CodMagnus ?? Constants.NENHUM_REGISTRO,
                                            Peca = p.Peca.NomePeca ?? Constants.NENHUM_REGISTRO,
                                            QuantidadePecas = p.QtdePecas
                                        }
                                    ))));

            var exportOsOld = os.SelectMany(os => os.RelatoriosAtendimento
                                        .SelectMany(r => r.RelatorioAtendimentoDetalhes
                                        .SelectMany(d => d.RelatorioAtendimentoDetalhePecas
                                        .Select(p =>
                                        new
                                        {
                                            Chamado = os.CodOS,
                                            NumOSCliente = os.NumOSCliente,
                                            DataAbertura = os.DataHoraAberturaOS?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                            DataSolicitacao = os.DataHoraSolicitacao?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                            LimiteAtendimento = os.PrazosAtendimento?.OrderByDescending(i => i.CodOSPrazoAtendimento)?.FirstOrDefault()?.DataHoraLimiteAtendimento?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                            Status = os.StatusServico?.NomeStatusServico?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Intervencao = os.TipoIntervencao?.CodETipoIntervencao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Tecnico = os.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                                            NumBanco = os.Cliente?.NumBanco ?? Constants.NENHUM_REGISTRO,
                                            NumAgencia = os.LocalAtendimento != null ? ($"{os.LocalAtendimento.NumAgencia}/{os.LocalAtendimento.DCPosto}") : Constants.NENHUM_REGISTRO,
                                            Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                            Regiao = os.Regiao?.NomeRegiao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            PA = os.RegiaoAutorizada?.PA ?? 0,
                                            Autorizada = os.Autorizada?.NomeFantasia?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Filial = os.Filial?.NomeFilial,
                                            SLA = os.EquipamentoContrato?.AcordoNivelServico?.NomeSLA ?? Constants.NENHUM_REGISTRO,
                                            Reincidencia = os.NumReincidencia ?? 0,
                                            Defeito = os.DefeitoRelatado ?? Constants.NENHUM_REGISTRO,
                                            Fechamento = os.DataHoraFechamento,

                                            CodRat = r.CodRAT,
                                            NumRat = r.NumRAT ?? Constants.NENHUM_REGISTRO,
                                            DataInicio = r.DataHoraInicio.Date.ToString() ?? Constants.NENHUM_REGISTRO,
                                            DataSolucao = r.DataHoraSolucao.Date.ToString() ?? Constants.NENHUM_REGISTRO,
                                            Hora = r.DataHoraSolucao.ToString("HH:mm"),
                                            TipoServico = r.TipoServico?.NomeServico ?? Constants.NENHUM_REGISTRO,
                                            Observacao = r.ObsRAT ?? Constants.NENHUM_REGISTRO,
                                            RelatoSolucao = r.RelatoSolucao ?? Constants.NENHUM_REGISTRO,
                                            DataCadastro = r.DataHoraCad,

                                            CodTipoCausa = d.TipoCausa?.CodETipoCausa ?? Constants.NENHUM_REGISTRO,
                                            TipoCausa = d.TipoCausa?.NomeTipoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodGrupoCausa = d.GrupoCausa?.CodEGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            GrupoCausa = d.GrupoCausa?.NomeGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodDefeito = d.Defeito?.CodEDefeito ?? Constants.NENHUM_REGISTRO,
                                            CodCausa = d.Causa?.CodECausa ?? Constants.NENHUM_REGISTRO,
                                            Causa = d.Causa?.NomeCausa ?? Constants.NENHUM_REGISTRO,
                                            CodAcao = d.Acao?.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                            Acao = d.Acao?.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                            OrigemCausa = d.CodOrigemCausa != null ? d.CodOrigemCausa.Value == 1 ? "Máquina" : "Extra Máquina" : "Causa Origem Não Informada",

                                            Cliente = os.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                            CodMagnus = p.Peca.CodMagnus ?? Constants.NENHUM_REGISTRO,
                                            Peca = p.Peca.NomePeca ?? Constants.NENHUM_REGISTRO,
                                            QuantidadePecas = p.QtdePecas
                                        }
                                    ))));


            var wsOs = Workbook.Worksheets.Add("Chamados");
            wsOs.Cell(2, 1).Value = osSheet;
            WriteHeaders(osSheet.FirstOrDefault(), wsOs);

            if (ratSheet.Any())
            {
                var wsRat = Workbook.Worksheets.Add("RATs");
                wsRat.Cell(2, 1).Value = ratSheet;
                WriteHeaders(ratSheet.FirstOrDefault(), wsRat);

                var wsRatDetalhe = Workbook.Worksheets.Add("Detalhes");
                wsRatDetalhe.Cell(2, 1).Value = ratDetalheSheet;
                WriteHeaders(ratDetalheSheet.FirstOrDefault(), wsRatDetalhe);

                var wsRatDetalhePeca = Workbook.Worksheets.Add("Peças");
                wsRatDetalhePeca.Cell(2, 1).Value = ratDetalhePecaSheet;
                WriteHeaders(ratDetalhePecaSheet.FirstOrDefault(), wsRatDetalhePeca);

                var wsOld = Workbook.Worksheets.Add("Exportação Legado");
                wsOld.Cell(2, 1).Value = exportOsOld;
                WriteHeaders(exportOsOld.FirstOrDefault(), wsOld);
            }
        }
    }
}
