using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        /// <summary>
        /// Roda via Agendamento
        /// </summary>
        public void CriaIntervalosDoDia()
        {
            Tecnico[] tecnicos = this._tecnicoRepo.ObterPorParametros(new TecnicoParameters
            {
                Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                IndAtivo = 1
            }).Where(t => t.CodTecnico.GetValueOrDefault(0) != 0).ToArray();

            foreach (Tecnico tecnicoAtivo in tecnicos)
            {
                if (!this._agendaRepo.ExisteIntervaloNoDia(tecnicoAtivo.CodTecnico.Value))
                {
                    AgendaTecnico novoIntervalo = new AgendaTecnico
                    {
                        CodTecnico = tecnicoAtivo.CodTecnico,
                        CodUsuarioCad = Constants.AGENDADOR_NOME,
                        DataHoraCad = DateTime.Now,
                        Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.INTERVALO),
                        Tipo = AgendaTecnicoTypeEnum.INTERVALO,
                        Titulo = this.IntervaloTitle,
                        Inicio = this.InicioIntervalo(),
                        Fim = this.FimIntervalo(),
                        IndAgendamento = 0,
                        IndAtivo = 1
                    };

                    this._agendaRepo.Criar(novoIntervalo);
                }
            }
        }
    }
}