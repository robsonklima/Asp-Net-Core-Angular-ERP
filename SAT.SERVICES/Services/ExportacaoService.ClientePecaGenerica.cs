using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaClientePecaGenerica(ClientePecaGenericaParameters parameters)
		{
            var clientePecaGenericas = _clientePecaGenericaRepo.ObterPorParametros(parameters);
            var sheet = clientePecaGenericas.Select(pg =>
                            new
                            {
                                Peca = pg.Peca?.NomePeca,
                                CodMagnus = pg.Peca?.CodMagnus,
                                ValorUnitario = pg.ValorUnitario,
                                
                            });

            var wsOs = Workbook.Worksheets.Add("clientePecaGenericas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}