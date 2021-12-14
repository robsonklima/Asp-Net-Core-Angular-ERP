using System.Linq;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public void DeletarAgendaTecnico(int codOS, int codTecnico)
        {
            var ag = this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
            {
                CodTecnico = codTecnico,
                CodOS = codOS,
                IndAtivo = 1
            }).ToList();

            if (ag != null)
                ag.ForEach(a =>
                {
                    a.IndAtivo = 0;
                    this._agendaRepo.Atualizar(a);
                });
        }
    }
}