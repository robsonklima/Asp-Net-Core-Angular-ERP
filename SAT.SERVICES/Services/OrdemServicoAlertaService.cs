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

            alertas.AddRange(ObterChamadosMesmoEquip(os, codOrdemServico));

            return alertas;
        }

        public List<Alerta> ObterChamadosMesmoEquip(OrdemServico os, int codOrdemServico)
        {
            var osEquip = _ordemServicoRepo
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        CodEquipContrato = os.CodEquipContrato,
                        NotIn_CodStatusServicos = "2,3,16"
                    });

            if (!osEquip.Any()) return null;

            return osEquip.Where(c => c.CodOS != codOrdemServico).Select(c =>
            {
                return new Alerta
                {
                    Titulo = "Mais de um chamado aberto para este equipamento",
                    Descricao = $"{c.CodOS} - {c.TipoIntervencao.NomTipoIntervencao}",
                    Tipo = Constants.CHAMADOS_MESMO_EQUIP,

                };
            }).ToList();
        }
    }
}