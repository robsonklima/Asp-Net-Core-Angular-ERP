using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using System.Linq;

namespace SAT.SERVICES.Interfaces
{
    public interface IExportacaoService
    {
        dynamic Exportar(ExportacaoParameters parameters);
    }
}