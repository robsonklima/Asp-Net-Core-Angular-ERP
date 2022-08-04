using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaLiderTecnico(LiderTecnicoParameters parameters)
		{
            var liderTecnico = _liderTecnicoRepo.ObterPorParametros(parameters);
            var sheet = liderTecnico.Select(lt =>
                            new 
                            {
                                Lider = lt.UsuarioLider?.NomeUsuario ?? Constants.NENHUM_REGISTRO,
                                Tecnico = lt.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("LiderTecnico");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}