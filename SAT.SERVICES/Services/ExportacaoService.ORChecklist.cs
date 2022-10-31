using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaORCheckList(ORCheckListParameters parameters)
		{
            var data = _orCheckListRepo.ObterPorParametros(parameters);
            var sheet = data.Select(c =>
                            new
                            {
                                Codigo = c.CodORCheckList
                            });

            var wsOs = Workbook.Worksheets.Add("Checklists");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}