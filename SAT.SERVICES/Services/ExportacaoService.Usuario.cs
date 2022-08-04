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
                                Perfil = u.Perfil?.NomePerfil ?? Constants.NENHUM_REGISTRO,
                       
                            });

            var wsOs = Workbook.Worksheets.Add("Usuario");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}