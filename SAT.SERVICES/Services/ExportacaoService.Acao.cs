using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaAcao(AcaoParameters parameters)
		{
            var acoes = _acaoRepo.ObterPorParametros(parameters);
            var sheet = acoes.Select(acao =>
                            new
                            {
                                CodAcao = acao.CodAcao,
                                CodEAcao = acao.CodEAcao,
                                NomeAcao = acao.NomeAcao,
                                IndPeca = acao.IndPeca,
                                IndAtivo = acao.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("Acoes");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}