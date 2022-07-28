using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaCliente(ClienteParameters parameters)
		{
            var clientes = _clienteRepo.ObterPorParametros(parameters);
            var sheet = clientes.Select(cliente =>
                            new
                            {
                                RazaoSocial = cliente.RazaoSocial,
                                NomeFantasia = cliente.NomeFantasia,
                                CNPJ = cliente.Cnpj?? Constants.NENHUM_REGISTRO,
                                Insc_Estadual = cliente.InscricaoEstadual?? Constants.NENHUM_REGISTRO,
                                Endereco = cliente.Endereco?? Constants.NENHUM_REGISTRO,
                                Bairro = cliente.Bairro?? Constants.NENHUM_REGISTRO,
                                CEP = cliente.Cep?? Constants.NENHUM_REGISTRO,
                                Cidade = cliente.Cidade?.NomeCidade?? Constants.NENHUM_REGISTRO,
                                UF = cliente.SiglaUF?? Constants.NENHUM_REGISTRO,
                                Ativo = cliente.IndAtivo == 1 ? "SIM" : "NÃO"
                            });

            var wsOs = Workbook.Worksheets.Add("clientes");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}