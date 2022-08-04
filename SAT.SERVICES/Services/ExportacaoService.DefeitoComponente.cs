using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDefeitoComponente(DefeitoComponenteParameters parameters)
		{
            var defeitoComponente = _defeitoComponenteRepo.ObterPorParametros(parameters);
            var sheet = defeitoComponente.Select(dc =>
                            new 
                            {
                                NomeCausa = dc.Causa?.NomeCausa?? Constants.NENHUM_REGISTRO,
                                CodECausa = dc.CodECausa?? Constants.NENHUM_REGISTRO,
                                NomeDefeito = dc.Defeito?.NomeDefeito?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("DefeitoCausas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}