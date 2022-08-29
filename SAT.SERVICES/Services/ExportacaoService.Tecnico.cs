using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaTecnico(TecnicoParameters parameters)
		{
            var tecnicos = _tecnicoRepo.ObterPorParametros(parameters);
            var sheet = tecnicos.Select(tecnico =>
                            new
                            {
                                Codigo = tecnico.CodTecnico,
                                Nome = tecnico.Nome ?? Constants.NENHUM_REGISTRO,
                                Filial = tecnico.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                FonePerto = tecnico.FonePerto ?? Constants.NENHUM_REGISTRO,
                                FoneParticular = tecnico.FoneParticular ?? Constants.NENHUM_REGISTRO,
                                DataAdmissao = tecnico.DataAdmissao,
                                IndAtivo = tecnico.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("Tecnicos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}