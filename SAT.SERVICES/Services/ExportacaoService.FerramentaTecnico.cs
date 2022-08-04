using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaFerramentaTecnico(FerramentaTecnicoParameters parameters)
		{
            var ferramentaTecnico = _ferramentaTecnicoRepo.ObterPorParametros(parameters);
            var sheet = ferramentaTecnico.Select(ft =>
                            new 
                            {
                                NomeCausa = ft.Nome?? Constants.NENHUM_REGISTRO,
                                Status = ft.Status == 1 ? "ATIVO" : "INATIVO",
                       
                            });

            var wsOs = Workbook.Worksheets.Add("FerramentaTecnico");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}