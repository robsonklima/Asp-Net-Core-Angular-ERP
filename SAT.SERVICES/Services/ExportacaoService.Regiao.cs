using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaRegiao(RegiaoParameters parameters)
		{
            var regiao = _regiaoRepo.ObterPorParametros(parameters);
            var sheet = regiao.Select(r =>
                            new 
                            {
                                Nome = r.NomeRegiao ?? Constants.NENHUM_REGISTRO,
                                Ativo = r.IndAtivo == 1 ? "SIM" : "NÃO",
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Regiao");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}