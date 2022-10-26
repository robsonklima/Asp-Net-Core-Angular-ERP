using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOrdemReparo(ORParameters parameters)
		{
            var clientes = _orRepo.ObterPorParametros(parameters);
            var sheet = clientes.Select(r =>
                            new
                            {
                                Codigo = r.CodOR
                            });

            var wsOs = Workbook.Worksheets.Add("reparos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}