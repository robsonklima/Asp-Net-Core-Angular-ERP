using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public AgendaTecnico CriarAgendaTecnico(int codOS, int codTecnico)
        {
            var os = this._osRepo.ObterPorCodigo(codOS);
            this.DeletarAgendaTecnico(codOS);
            var inicioPeriodo = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0));
            var fimPeriodo = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
            var mediaTecnico = 60;

            var agendas = this.ObterAgenda(inicioPeriodo, fimPeriodo, codTecnico);
            var ultimaAgenda = ObterUltimaAgenda(agendas, codTecnico);

            var deslocamento = this.DistanciaEmMinutos(os, ultimaAgenda?.OrdemServico);
            var inicio = ultimaAgenda != null ? ultimaAgenda.Fim : DateTime.Now;
            inicio = inicio.AddMinutes(deslocamento);

            if (this.estaNoIntervalo(inicio)) {
                inicio = this.FimIntervalo(inicio);
            }

            var fim = inicio.AddMinutes(mediaTecnico);
            if (this.estaNoIntervalo(fim))
            {
                inicio = fim.AddMinutes(deslocamento);
                fim = inicio.AddMinutes(mediaTecnico);
            }

            if (inicio > this.FimExpediente()) {
                inicio = DateTime.Now.AddDays(1).Date.Add(new TimeSpan(8, 0, 0));
                fim = inicio.Date.Add(new TimeSpan(9, 0, 0));
            }

            return InserirAgendaDB(inicio, fim, codTecnico, os);
        }

        private AgendaTecnico ObterUltimaAgenda(List<AgendaTecnico> agendas, int codTecnico)
        {
            return agendas
                .Where(
                    i => i.CodTecnico == codTecnico &&
                    i.Tipo == AgendaTecnicoTypeEnum.OS &&
                    i.IndAgendamento == 0 &&
                    i.Inicio.Date == DateTime.Now.Date
                )
                .OrderByDescending(i => i.Fim)
                .FirstOrDefault();
        }

        private AgendaTecnico InserirAgendaDB(DateTime inicio, DateTime fim, int codTecnico, OrdemServico os) {
            var agendaTecnico = new AgendaTecnico
            {
                Inicio = inicio,
                Fim = fim,
                Titulo = os.LocalAtendimento?.NomeLocal?.ToUpper(),
                Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.OS),
                CodOS = os.CodOS,
                CodTecnico = codTecnico,
                Tipo = AgendaTecnicoTypeEnum.OS,
                IndAgendamento = 0,
                IndAtivo = 1,
                CodUsuarioCad = Constants.SISTEMA_NOME,
                DataHoraCad = DateTime.Now
            };

            return this._agendaRepo.Criar(agendaTecnico);
        }
    }
}