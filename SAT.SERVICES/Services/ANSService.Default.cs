using System;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        private DateTime CalcularSLADefault()
        {
            return DateTime.Now.AddDays(30);
        }
    }
}