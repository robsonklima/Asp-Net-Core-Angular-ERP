using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaEquipamentoModulo(EquipamentoModuloParameters parameters)
		{
            var equipamentoModulo = _equipamentoModuloRepo.ObterPorParametros(parameters);
            var sheet = equipamentoModulo.Select(em =>
                            new 
                            {
                                NomeEquipamento = em.Equipamento?.DescEquip?? Constants.NENHUM_REGISTRO,
                                TipoEquipamento = em.Equipamento?.TipoEquipamento?.NomeTipoEquip?? Constants.NENHUM_REGISTRO,
                                Ativo = em.IndAtivo == 1 ? "SIM" : "NÃO",
                       
                            });

            var wsOs = Workbook.Worksheets.Add("EquipamentoModulo");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}