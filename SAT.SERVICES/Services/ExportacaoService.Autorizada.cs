using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaAutorizada(AutorizadaParameters parameters)
		{
            var autorizadas = _autorizadaRepo.ObterPorParametros(parameters);
            var sheet = autorizadas.Select(autorizada =>
                            new
                            {
                                codAutorizada = autorizada.CodAutorizada,
                                razaosocial = autorizada.RazaoSocial,
                                nomeFantasia = autorizada.NomeFantasia,
                                IndAtivo = autorizada.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("Autorizadas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}