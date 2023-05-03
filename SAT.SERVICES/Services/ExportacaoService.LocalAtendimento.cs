using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaLocalAtendimento(LocalAtendimentoParameters parameters)
		{
            var localAtendimento = _localAtendimentoRepo.ObterPorParametros(parameters);
            var sheet = localAtendimento.Select(la =>
                            new 
                            {
                                Agencia = la.NumAgencia ?? Constants.NENHUM_REGISTRO,
                                Posto = la.DCPosto ?? Constants.NENHUM_REGISTRO,
                                NomeLocal = la.NomeLocal ?? Constants.NENHUM_REGISTRO,
                                Clilente = la.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                Filial = la.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                Ativo = la.IndAtivo == 1 ? "SIM" : "NÃO",
                                Endereco = la.Endereco ?? Constants.NENHUM_REGISTRO,
                                Numero = la.NumeroEnd ?? Constants.NENHUM_REGISTRO,
                                Bairro = la.Bairro ?? Constants.NENHUM_REGISTRO,
                                Cidade = la.Cidade?.NomeCidade ?? Constants.NENHUM_REGISTRO,
                                UF = la.Cidade?.UnidadeFederativa?.SiglaUF ?? Constants.NENHUM_REGISTRO,
                                CEP = la.Cep ?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("LocalAtendimento");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}