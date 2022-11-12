using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaAuditoria(AuditoriaParameters parameters)
		{
            var auditoria = _auditoriaRepo.ObterPorView(parameters);
            var sheet = auditoria.Select(aud =>
                            new 
                            {
                                Codigo = aud.CodAuditoria,
                                Usuario = aud.NomeUsuario,
                                Status = aud.NomeAuditoriaStatus,
                                NomeFilial = aud.NomeFilial,
                                QtdDiasAuditoriaAnterior = aud.QtdDiasAuditoriaAnterior,
                                QtdDespesasPendentes = aud.QtdDespesasPendentes,
                                OdometroAnterior = aud.OdometroAnterior,
                                OdometroAtual = aud.OdometroAtual,
                                QuilometrosPorLitro = aud.QuilometrosPorLitro
                            });

            var wsOs = Workbook.Worksheets.Add("auditoria");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}