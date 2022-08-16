using SAT.MODELS.Entities;
using SAT.MODELS.Enums;

namespace SAT.SERVICES.Interfaces
{
    public interface IExportacaoService
    {
        dynamic Exportar(Exportacao exportacao);
    }
}