using System;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        private DateTime CalcularSLABB(OrdemServico chamado)
        {
            return DateTime.Now.AddDays(30);
        }
    }
}