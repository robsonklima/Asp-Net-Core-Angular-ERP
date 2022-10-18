using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOR(ORParameters parameters)
        {
            var reparos = _orRepo.ObterPorParametros(parameters);
            var sheet = reparos.Select(reparo => new {
                CodOR = reparo.CodOR
            });

            var wsOs = Workbook.Worksheets.Add("orcamentos");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}