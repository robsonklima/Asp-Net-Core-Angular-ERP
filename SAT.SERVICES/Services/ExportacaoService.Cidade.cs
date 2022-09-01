using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaCidade(CidadeParameters parameters)
		{
            var cidades = _cidadeRepo.ObterPorParametros(parameters);
            var sheet = cidades.Select(cidade =>
                            new
                            {
                                CodCidade = cidade.CodCidade,
                                Nome = cidade.NomeCidade,
                                UF = cidade.UnidadeFederativa?.NomeUF?? Constants.NENHUM_REGISTRO,
                                Ativo = cidade.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("Cidades");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}