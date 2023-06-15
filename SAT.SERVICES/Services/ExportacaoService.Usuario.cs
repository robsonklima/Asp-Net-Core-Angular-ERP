using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaUsuario(UsuarioParameters parameters)
		{
            var usuario = _usuarioRepo.ObterPorParametros(parameters);
            var sheet = usuario.Select(u =>
                            new 
                            {
                                CodUsuario = u.CodUsuario ?? Constants.NENHUM_REGISTRO,
                                Nome = u.NomeUsuario ?? Constants.NENHUM_REGISTRO,
                                Cargo = u.Cargo?.NomeCargo ?? Constants.NENHUM_REGISTRO,
                                Setor = u.Setor?.NomeSetor ?? Constants.NENHUM_REGISTRO,
                                Perfil = u.Perfil?.NomePerfil ?? Constants.NENHUM_REGISTRO,
                                Filial = u.Filial.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                CPF = u.Cpf ?? Constants.NENHUM_REGISTRO,
                                Endereco = u.Endereco ?? Constants.NENHUM_REGISTRO,
                                Bairro = u.Bairro ?? Constants.NENHUM_REGISTRO,
                                Cidade = u.Cidade.NomeCidade ?? Constants.NENHUM_REGISTRO,
                                UF = u.Cidade.UnidadeFederativa.SiglaUF ?? Constants.NENHUM_REGISTRO,
                                Pais = u.Cidade.UnidadeFederativa.Pais.SiglaPais ?? Constants.NENHUM_REGISTRO,
                            });

            var wsOs = Workbook.Worksheets.Add("Usuario");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}