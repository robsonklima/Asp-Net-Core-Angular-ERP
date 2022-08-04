using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaFeriado(FeriadoParameters parameters)
		{
            var feriado = _feriadoRepo.ObterPorParametros(parameters);
            var sheet = feriado.Select(f =>
                            new 
                            {
                                NomeFeriado = f.NomeFeriado?? Constants.NENHUM_REGISTRO,
                                //Data = f.Data.HasValue ? f.Data.Value.ToString("dd/MM/yyyy") : Constants.NENHUM_REGISTRO,
                                Uf = f.UnidadeFederativa.NomeUF?? Constants.NENHUM_REGISTRO,
                                Cidade = f.Cidade.NomeCidade?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Feriado");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}