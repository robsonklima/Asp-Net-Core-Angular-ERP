using SAT.MODELS.Enums;

namespace SAT.SERVICES.Interfaces
{
    public interface IExportacaoService
    {
        dynamic Exportar(dynamic parameters, ExportacaoFormatoEnum formato, ExportacaoTipoEnum tipo);
    }
}