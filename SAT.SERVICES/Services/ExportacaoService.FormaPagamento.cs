using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaFormaPagamento(FormaPagamentoParameters parameters)
		{
            var formaPagamento = _formaPagamentoRepo.ObterPorParametros(parameters);
            var sheet = formaPagamento.Select(fp =>
                            new 
                            {
                                Nome = fp.DescFormaPagto ?? Constants.NENHUM_REGISTRO,
                                PercAjuste = fp.PercAjuste, Constants.NENHUM_REGISTRO,
                                Ativo = fp.IndAtivo == 1 ? "SIM" : "NÃO",
                       
                            });

            var wsOs = Workbook.Worksheets.Add("FormaPagamento");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}