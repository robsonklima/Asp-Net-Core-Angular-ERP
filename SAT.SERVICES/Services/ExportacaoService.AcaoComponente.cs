using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaAcaoComponente(AcaoComponenteParameters parameters)
		{
            var acoesComponente = _acaoComponenteRepo.ObterPorParametros(parameters);

            var sheet = acoesComponente.Select(ac =>
                            new
                            {
                                CodAcao = ac.CodAcao,
                                NomeCausa = ac.Causa?.NomeCausa?? Constants.NENHUM_REGISTRO,
                
                            });

            var wsOs = Workbook.Worksheets.Add("AcoesCausas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}