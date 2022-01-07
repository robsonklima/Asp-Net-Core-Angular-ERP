using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        /// <summary>
        /// Roda via Agendamento
        /// </summary>
        public void AtualizaMediaTecnico()
        {
            this._mediaTecnicoRepo.AtualizaMediaTecnico();
        }
    }
}