using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaGrupoEquipamento(GrupoEquipamentoParameters parameters)
		{
            var grupoEquipamentos = _grupoEquipamentoRepo.ObterPorParametros(parameters);
            var sheet = grupoEquipamentos.Select(ge =>
                            new
                            {
                                Codigo = ge.CodEGrupoEquip,
                                Grupo = ge.NomeGrupoEquip,
                                Tipo = ge.TipoEquipamento?.NomeTipoEquip,

                            });

            var wsOs = Workbook.Worksheets.Add("grupoEquipamentos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}