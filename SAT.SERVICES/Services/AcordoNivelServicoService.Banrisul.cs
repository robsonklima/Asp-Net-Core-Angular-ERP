using System;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AcordoNivelServicoService : IAcordoNivelServicoService
    {
        private DateTime CalcularSLABanrisul(OrdemServico os)
        {
            return DateTime.Now.AddDays(30);
        }
    }
}