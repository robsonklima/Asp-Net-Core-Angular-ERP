using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaClientePeca(ClientePecaParameters parameters)
		{
            var clientePecas = _clientePecaRepo.ObterPorParametros(parameters);
            var sheet = clientePecas.Select(cp =>
                            new
                            {
                                Peca = cp.Peca.NomePeca,
                                CodMagnus = cp.Peca.CodMagnus,
                                ValorUnitario = cp.ValorUnitario,
                                ValorIPI = cp.ValorIPI,
                                Cliente = cp.Cliente?.NomeFantasia?? Constants.NENHUM_REGISTRO,
                                Contrato = cp.Contrato?.NomeContrato?? Constants.NENHUM_REGISTRO,
                                Status = cp.Peca.PecaStatus?.NomeStatus?? Constants.NENHUM_REGISTRO,
                                
                                
                            });

            var wsOs = Workbook.Worksheets.Add("clientePecas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}