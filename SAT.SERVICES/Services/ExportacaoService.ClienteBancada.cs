using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaClienteBancada(ClienteBancadaParameters parameters)
		{
            var clienteBancadas = _clienteBancadaRepo.ObterPorParametros(parameters);
            var sheet = clienteBancadas.Select(cb =>
                            new
                            {
                                Cliente = cb.NomeCliente,
                                Apelido = cb.Apelido,
                                CNPJ_CGC = cb.CNPJ_CGC?? Constants.NENHUM_REGISTRO,
                                Endereco = cb.Endereco?? Constants.NENHUM_REGISTRO,
                                Numero = cb.Numero?? Constants.NENHUM_REGISTRO,
                                Bairro = cb.Bairro?? Constants.NENHUM_REGISTRO,
                                CEP = cb.Cep?? Constants.NENHUM_REGISTRO,
                                Cidade = cb.Cidade?.NomeCidade?? Constants.NENHUM_REGISTRO,
                                UF = cb.Cidade?.UnidadeFederativa?.SiglaUF?? Constants.NENHUM_REGISTRO,
                                Contato = cb.Contato?? Constants.NENHUM_REGISTRO,
                                Email = cb.Email?? Constants.NENHUM_REGISTRO,
                                Telefone = cb.Telefone?? Constants.NENHUM_REGISTRO,
                                Ativo = cb.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("clienteBancadas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}