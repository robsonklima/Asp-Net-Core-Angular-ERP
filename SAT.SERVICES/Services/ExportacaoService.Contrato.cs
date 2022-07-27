using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaContrato(ContratoParameters parameters)
		{
            var contratos = _contratoRepo.ObterPorParametros(parameters);
            var sheet = contratos.Select(contrato =>
                            new
                            {
                                NroContrato = contrato.NroContrato,
                                Contrato = contrato.NomeContrato,
                                Cliente = contrato.Cliente?.RazaoSocial?? Constants.NENHUM_REGISTRO,
                                TipoContrato = contrato.TipoContrato?.NomeTipoContrato?? Constants.NENHUM_REGISTRO,
                                Ativo = contrato.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("contratos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}