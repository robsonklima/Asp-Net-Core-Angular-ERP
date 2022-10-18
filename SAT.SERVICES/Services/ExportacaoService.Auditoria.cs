using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaAuditoria(AuditoriaParameters parameters)
		{
            var auditoria = _auditoriaRepo.ObterPorParametros(parameters);
            var sheet = auditoria.Select(aud =>
                            new 
                            {
                                Codigo = aud.CodAuditoria,
                                Tecnico = aud.Usuario?.NomeUsuario,
                                Veiculo = aud.AuditoriaVeiculo?.Placa ?? Constants.NENHUM_REGISTRO,
                                DataCadastro = aud.DataHoraCad?.ToString("dd/MM/yy HH:mm"),
                                DataRetiradaVeiculo = aud.DataHoraRetiradaVeiculo?.ToString("dd/MM/yy HH:mm"),
                                Status = aud.AuditoriaStatus.Nome,
                                DiasEmUso = aud?.TotalDiasEmUso,
                                MesesEmUso = aud?.TotalMesesEmUso,
                                ValorCombustível = aud?.ValorCombustivel,
                                CreditosCartão = aud?.CreditosCartao,
                                DespesasSAT = aud?.DespesasSAT,
                                DespesasCompensadas = aud?.DespesasCompensadasValor,
                                OdometroInicialRetirada = aud?.OdometroInicialRetirada,
                                OdometroPeriodoAuditado = aud?.OdometroPeriodoAuditado,
                                SaldaCartão = aud?.SaldoCartao,
                                KmPercorrido = aud?.KmPercorrido,
                                KmCompensado = aud?.KmCompensado,
                                ValorTanque = aud?.ValorTanque,
                                KmFérias = aud.KmFerias,
                                UsoParticulas = aud.UsoParticular,
                                KmParticular = aud?.KmParticular,
                                KmParticularMês = aud?.KmParticularMes,
                                Observações = aud?.Observacoes,
                                DataHoraManut = aud?.DataHoraManut?.ToString("dd/MM/yy HH:mm"),
                                UsuarioManut = aud?.CodUsuarioManut,
                            });

            var wsOs = Workbook.Worksheets.Add("auditoria");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}