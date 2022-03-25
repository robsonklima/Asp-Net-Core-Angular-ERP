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
								 DataAbertura = os.DataHoraAberturaOS?.ToString("dd/MM/yy HH:mm") ?? Constants.SEM_NADA,
								 DataSolicitacao = os.DataHoraSolicitacao?.ToString("dd/MM/yy HH:mm") ?? Constants.SEM_NADA,
								 LimiteAtendimento = os.PrazosAtendimento?.OrderByDescending(i => i.CodOSPrazoAtendimento)?.FirstOrDefault()?.DataHoraLimiteAtendimento?.ToString("dd/MM/yy HH:mm") ?? Constants.SEM_NADA,
								 Status = os.StatusServico?.NomeStatusServico?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 Intervencao = os.TipoIntervencao?.CodETipoIntervencao?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 Tecnico = os.Tecnico?.Nome ?? Constants.SEM_NADA,
								 NumBanco = os.Cliente?.NumBanco ?? Constants.SEM_NADA,
								 NumAgencia = os.LocalAtendimento != null ? ($"{os.LocalAtendimento.NumAgencia}/{os.LocalAtendimento.DCPosto}") : Constants.SEM_NADA,
								 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 Equipamento = os.Equipamento?.NomeEquip ?? Constants.SEM_NADA,
								 Serie = os.EquipamentoContrato?.NumSerie ?? Constants.SEM_NADA,
								 Regiao = os.Regiao?.NomeRegiao?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 PA = os.RegiaoAutorizada?.PA ?? 0,
								 Autorizada = os.Autorizada?.NomeFantasia?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 Filial = os.Filial?.NomeFilial,
								 SLA = os.EquipamentoContrato?.AcordoNivelServico?.NomeSLA ?? Constants.SEM_NADA,
								 Reincidencia = os.NumReincidencia ?? 0,
								 Defeito = os.DefeitoRelatado ?? Constants.SEM_NADA
							 });

			var ratSheet = os.SelectMany(os => os.RelatoriosAtendimento.Select(r =>
							 new
							 {
								 Chamado = r.CodOS,
								 CodRat = r.CodRAT,
								 NumRat = r.NumRAT ?? Constants.SEM_NADA,
								 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.SEM_NADA,
								 Tecnico = r.Tecnico?.Nome ?? Constants.SEM_NADA,
								 Status = r.StatusServico?.NomeStatusServico ?? Constants.SEM_NADA,
								 DataInicio = r.DataHoraInicio.Date.ToString() ?? Constants.SEM_NADA,
								 DataSolucao = r.DataHoraSolucao.Date.ToString() ?? Constants.SEM_NADA,
								 Hora = r.DataHoraSolucao.ToString("HH:mm"),
								 TipoServico = r.TipoServico?.NomeServico ?? Constants.SEM_NADA,
								 Observacao = r.ObsRAT ?? Constants.SEM_NADA,
								 RelatoSolucao = r.RelatoSolucao ?? Constants.SEM_NADA,
							 }
						));

			var ratDetalheSheet = os.SelectMany(os => os.RelatoriosAtendimento
														.SelectMany(rat => rat.RelatorioAtendimentoDetalhes
																			.Select(d =>
																				new
																				{
																					Chamado = rat?.CodOS,
																					Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.SEM_NADA,
																					NumRat = rat?.CodRAT,
																					CodTipoCausa = d.TipoCausa?.CodETipoCausa ?? Constants.SEM_NADA,
																					TipoCausa = d.TipoCausa?.NomeTipoCausa ?? Constants.SEM_NADA,
																					CodGrupoCausa = d.GrupoCausa?.CodEGrupoCausa ?? Constants.SEM_NADA,
																					GrupoCausa = d.GrupoCausa?.NomeGrupoCausa ?? Constants.SEM_NADA,
																					CodDefeito = d.Defeito?.CodEDefeito ?? Constants.SEM_NADA,
																					Defeito = d.Defeito?.NomeDefeito ?? Constants.SEM_NADA,
																					CodCausa = d.Causa?.CodECausa ?? Constants.SEM_NADA,
																					Causa = d.Causa?.NomeCausa ?? Constants.SEM_NADA,
																					CodAcao = d.Acao?.CodEAcao ?? Constants.SEM_NADA,
																					Acao = d.Acao?.NomeAcao ?? Constants.SEM_NADA,
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
																										Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.SEM_NADA,
																										NumRat = rat.NumRAT ?? Constants.SEM_NADA,
																										CodAcao = rp.Acao?.CodEAcao ?? Constants.SEM_NADA,
																										Acao = rp.Acao?.NomeAcao ?? Constants.SEM_NADA,
																										CodMagnus = p.Peca.CodMagnus ?? Constants.SEM_NADA,
																										Peca = p.Peca.NomePeca ?? Constants.SEM_NADA,
																										QuantidadePecas = p.QtdePecas
																									}
																			))));

			var wsOs = Workbook.Worksheets.Add("Chamados");
			var wsRat = Workbook.Worksheets.Add("RATs");
			var wsRatDetalhe = Workbook.Worksheets.Add("Detalhes");
			var wsRatDetalhePeca = Workbook.Worksheets.Add("Peças");

			wsOs.Cell(2, 1).Value = osSheet;
			wsRat.Cell(2, 1).Value = ratSheet;
			wsRatDetalhe.Cell(2, 1).Value = ratDetalheSheet;
			wsRatDetalhePeca.Cell(2, 1).Value = ratDetalhePecaSheet;

			WriteHeaders(osSheet.FirstOrDefault(), wsOs);
			if (ratSheet != null)
			{
				WriteHeaders(ratSheet.FirstOrDefault(), wsRat);
				WriteHeaders(ratDetalheSheet.FirstOrDefault(), wsRatDetalhe);
				WriteHeaders(ratDetalhePecaSheet.FirstOrDefault(), wsRatDetalhePeca);
			}
		}
	}
}
