using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaRegiaoAutorizada(RegiaoAutorizadaParameters parameters)
		{
            var regiaoAutorizada = _regiaoAutorizadaRepo.ObterPorParametros(parameters);
            var sheet = regiaoAutorizada.Select(ra =>
                            new 
                            {
                                Regiao = ra.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO,
                                Autorizada = ra.Autorizada?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                Cidade = ra.Cidade?.NomeCidade ?? Constants.NENHUM_REGISTRO,
                                Filial = ra.Filial?.NomeFilial ??Constants.NENHUM_REGISTRO,
                                Ativo = ra.IndAtivo == 1 ? "SIM" : "NÃO",
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Regiao");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}