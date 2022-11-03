using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDespesaCartaoCombustivel(DespesaCartaoCombustivelParameters parameters)
		{
            var query = _despesaCartaoCombustivelRepo.ObterPorParametros(parameters);
            var sheet = query.Select(data =>
                            new
                            {
                                Numero = data.Numero,
                                Carro = data.Carro,
                                Placa = data.Placa,
                                Cor = data.Cor,
                                Ano = data.Ano,
                                Combustivel = data.Combustivel,
                                Ativo = data.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("contratos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}