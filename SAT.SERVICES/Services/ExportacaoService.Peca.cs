using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaPeca(PecaParameters parameters)
		{
            var peca = _pecaRepo.ObterPorParametros(parameters);
            var sheet = peca.Select(p =>
                            new 
                            {
                                NomePeca = p.NomePeca ?? Constants.NENHUM_REGISTRO,
                                Status = p.PecaStatus?.NomeStatus ?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Peca");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}