using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaEquipamento(EquipamentoParameters parameters)
		{
            var equipamentos = _equipamentoRepo.ObterPorParametros(parameters);
            var sheet = equipamentos.Select(e =>
                            new
                            {
                                Modelo = e.NomeEquip,
                                Descricao = e.DescEquip?? Constants.NENHUM_REGISTRO,
                                Tipo = e.TipoEquipamento?.NomeTipoEquip,
                                Grupo = e.GrupoEquipamento?.NomeGrupoEquip,

                            });

            var wsOs = Workbook.Worksheets.Add("equipamentos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}