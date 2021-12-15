using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoAlertaService : IOrdemServicoAlertaService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public OrdemServicoAlertaService(IOrdemServicoRepository ordemServicoRepo, ISequenciaRepository sequenciaRepo)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public List<Alerta> ObterAlertas(int codOrdemServico)
        {
            var os = _ordemServicoRepo.ObterPorCodigo(codOrdemServico);
            List<Alerta> alertas = new List<Alerta>();

            alertas.Add(ObterAvisoChamadoVizualizado(os));
            alertas.Add(ObterAvisoChamadosMesmoEquip(os));
            alertas.Add(ObterAvisoChamadosCidadePinpad(os));


            if (alertas.All(item => item == null)) return null;

            alertas.RemoveAll(al => al == null);

            return alertas;
        }

        private Alerta ObterAvisoChamadosCidadePinpad(OrdemServico os)
        {
            var osEquip = _ordemServicoRepo
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        NotIn_CodStatusServicos = "2,3,16"
                    })
                    .Where(c => c.CodOS != os.CodOS &&
                            c.LocalAtendimento.CodCidade.Equals(os.LocalAtendimento.CodCidade) &&
                            Constants.EQUIPAMENTOS_PINPAD.Any(i => i.Equals(c.CodEquip)))
                    .ToList();

            if (osEquip.Any())
            {
                var al = new Alerta
                {
                    Titulo = "Chamados de PINPAD para a mesma cidade ",
                    Descricao = new List<string>(),
                    Tipo = Constants.INFO,
                };

                osEquip.ForEach(e => 
                {
                    al.Descricao.Add($"{e.CodOS} - {e.TipoIntervencao.NomTipoIntervencao} - {e.StatusServico.NomeStatusServico}");
                });

                return al;
            }

            return null;
        }

        private Alerta ObterAvisoChamadoVizualizado(OrdemServico os)
        {
            if (os.CodStatusServico != 8) return null;

            var alerta = new Alerta
            {
                Titulo = "Chamado visualizado pelo técnico",
                Descricao = new List<string>(),
                Tipo = Constants.SUCCESS,
            };

            if (string.IsNullOrEmpty(os.CodUsuarioOSMobileLida))
            {
                alerta.Titulo = "Chamado ainda não visualizado";
                alerta.Descricao.Add($"Técnico Transferido: {os.Tecnico?.Nome}");
                alerta.Tipo = Constants.INFO;
                return alerta;
            }

            alerta.Descricao.Add($"Técnico: {os.Tecnico?.Nome} às {os.DataHoraOSMobileLida}");

            return alerta;
        }

        public Alerta ObterAvisoChamadosMesmoEquip(OrdemServico os)
        {
            var alerta = new Alerta
            {
                Titulo = "Mais de um chamado aberto para este equipamento",
                Descricao = new List<string>(),
                Tipo = Constants.WARNING,
            };

            var osEquip = _ordemServicoRepo
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        CodEquipContrato = os.CodEquipContrato,
                        NotIn_CodStatusServicos = "2,3,16"
                    }).Where(c => c.CodOS != os.CodOS).ToList();

            if (!osEquip.Any() || !os.CodEquipContrato.HasValue) return null;

            osEquip.ForEach(os =>
                    {
                        alerta.Descricao.Add($"{os.CodOS} - {os.TipoIntervencao.NomTipoIntervencao} - {os.StatusServico.NomeStatusServico}");
                    });

            return alerta;
        }
    }
}