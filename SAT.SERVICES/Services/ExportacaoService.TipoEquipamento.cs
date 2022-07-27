using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaTipoEquipamento(TipoEquipamentoParameters parameters)
		{
            var tipoEquipamentos = _tipoEquipamentoRepo.ObterPorParametros(parameters);
            var sheet = tipoEquipamentos.Select(tipo =>
                            new
                            {
                                Codigo = tipo.CodETipoEquip,
                                Tipo = tipo.NomeTipoEquip,

                            });

            var wsOs = Workbook.Worksheets.Add("tipoEquipamentos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}