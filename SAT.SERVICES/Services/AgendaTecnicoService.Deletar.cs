using System;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
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
                    a.CodUsuarioManut = Constants.SISTEMA_NOME;
                    a.DataHoraManut = DateTime.Now;
                    this._agendaRepo.Atualizar(a);
                });
        }

        public void DeletarAgendaTecnico(int codOS)
        {
            var ag = this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
            {
                CodOS = codOS,
                IndAtivo = 1
            }).ToList();

            if (ag != null)
                ag.ForEach(a =>
                {
                    a.IndAtivo = 0;
                    a.CodUsuarioManut = Constants.SISTEMA_NOME;
                    a.DataHoraManut = DateTime.Now;
                    this._agendaRepo.Atualizar(a);
                });
        }
    }
}