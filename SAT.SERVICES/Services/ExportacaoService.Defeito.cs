using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDefeito(DefeitoParameters parameters)
		{
            var defeitos = _defeitoRepo.ObterPorParametros(parameters);
            var sheet = defeitos.Select(defeito =>
                            new
                            {
                                CodDefeito = defeito.CodEDefeito,
                                Defeito = defeito.NomeDefeito,
                                Ativo = defeito.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("defeitos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}