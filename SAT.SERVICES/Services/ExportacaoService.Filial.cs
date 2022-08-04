using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaFilial(FilialParameters parameters)
		{
            var filial = _filialRepo.ObterPorParametros(parameters);
            var sheet = filial.Select(f =>
                            new 
                            {
                                Filial = f.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                RazaoSocial = f.RazaoSocial ?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Filial");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}