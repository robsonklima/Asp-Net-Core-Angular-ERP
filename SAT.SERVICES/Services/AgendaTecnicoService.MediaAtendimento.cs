using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        /// <summary>
        /// Roda via Agendamento
        /// </summary>
        public void AtualizaMediaTecnico()
        {
            Tecnico[] tecnicos = this._tecnicoRepo.ObterQuery(new TecnicoParameters
            {
                Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                IndAtivo = 1
            }).Where(t => t.CodTecnico.GetValueOrDefault(0) != 0).ToArray();

            Parallel.ForEach(tecnicos, tecnico =>
            {

                // var rats = this._ratRepo.
            });
        }
    }
}