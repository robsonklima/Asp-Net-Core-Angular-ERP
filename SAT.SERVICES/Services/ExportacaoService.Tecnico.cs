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
                                Codigo = tecnico?.CodTecnico,
                                Nome = tecnico.Nome ?? Constants.NENHUM_REGISTRO,
                                Filial = tecnico.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                Autorizada = tecnico?.Autorizada?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                Perfil = tecnico?.Usuario?.Perfil?.NomePerfil ?? Constants.NENHUM_REGISTRO,
                                FonePerto = tecnico?.FonePerto ?? Constants.NENHUM_REGISTRO,
                                FoneParticular = tecnico?.FoneParticular ?? Constants.NENHUM_REGISTRO,
                                DataAdmissao = tecnico?.DataAdmissao,
                                Ativo = tecnico?.IndAtivo == 1 ? "SIM" : "NÃO",
                                Cidade = tecnico?.Cidade?.NomeCidade ?? Constants.NENHUM_REGISTRO,
                                Regiao = tecnico?.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO
                            });

            var wsOs = Workbook.Worksheets.Add("Tecnicos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}