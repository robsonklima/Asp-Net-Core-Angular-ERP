using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DeslocamentoService : IDeslocamentoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;

        public DeslocamentoService(
            IOrdemServicoRepository ordemServicoRepo
        )
        {
            _ordemServicoRepo = ordemServicoRepo;
        }

        public IEnumerable<Deslocamento> ObterPorParametros(DeslocamentoParameters parameters)
        {
            var deslocamentos = new List<Deslocamento>();

            deslocamentos.AddRange(
                _ordemServicoRepo.ObterPorParametros(new OrdemServicoParameters() {
                    CodTecnico = 2410,
                    Include = OrdemServicoIncludeEnum.OS_INTENCAO,
                    DataHoraInicioInicio = DateTime.Now.AddDays(-15),
                    DataHoraInicioFim = DateTime.Now
                }).Select(os => new Deslocamento {
                    Origem = new DeslocamentoOrigem() {
                        Lat = os.Intencoes.FirstOrDefault().Latitude,
                        Lng = os.Intencoes.FirstOrDefault().Longitude,
                    },
                    Destino = new DeslocamentoDestino() {
                        Lat = Double.Parse(os.LocalAtendimento.Latitude),
                        Lng = Double.Parse(os.LocalAtendimento.Longitude),
                    },
                    Tipo = DeslocamentoTipoEnum.INTENCAO
                })
            );

            return deslocamentos;
        }
    }
}
