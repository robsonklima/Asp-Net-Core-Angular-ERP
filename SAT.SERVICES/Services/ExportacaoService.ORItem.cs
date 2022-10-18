using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaORItem(ORItemParameters parameters)
        {
            var items = _orItemRepo.ObterPorParametros(parameters);
            var sheet = items.Select(item => new {
                CodOR = item.CodOR,
                CodORItem = item.CodORItem,
                CodMagnus = item?.Peca?.CodMagnus,
                Descricao = item?.Peca?.NomePeca,
                Status = item?.StatusOR?.DescStatus,
                NumSerie = item.NumSerie,
                Cadastro = item.DataHoraCad,
                Filial = item?.OrdemServico?.Filial?.NomeFilial,
                Usuario = item?.UsuarioTecnico?.NomeUsuario,
                DiasEmReparo = item.DiasEmReparo
            });

            var wsOs = Workbook.Worksheets.Add("orcamentos");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}