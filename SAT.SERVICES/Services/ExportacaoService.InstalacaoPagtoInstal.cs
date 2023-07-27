using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaInstalacaoPagtoInstal(InstalacaoPagtoInstalParameters parameters)
		{
            var instalacoes = _instalPagtoIntalRepo.ObterPorParametros(parameters);
            var sheet = instalacoes.Select(i =>
                            new
                            {
                                Instalacao = i.Instalacao.CodInstalacao,
                                Num_Serie = i.Instalacao.EquipamentoContrato?.NumSerie,
                                Modelo = i.Instalacao.Equipamento?.NomeEquip,
                                Tipo_Parcela = i.InstalacaoTipoParcela?.NomeTipoParcela ?? Constants.NENHUM_REGISTRO,
                                Valor_Parcela = string.Format("{0:C}", i.VlrParcela),
                                Valor_Multa = string.Format("{0:C}", i.VlrMulta),
                                Motivo_Multa = i.InstalacaoMotivoMulta?.DescMotivoMulta ?? Constants.NENHUM_REGISTRO,
                                Endossar_Multa = i.IndEndossarMulta == 1 ? "SIM" : "NÃO" ?? Constants.NENHUM_REGISTRO,	
                                NF_Venda = i.Instalacao.InstalacaoNFVenda?.NumNFVenda,

                            });

            var wsOs = Workbook.Worksheets.Add("instalacoes");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}
