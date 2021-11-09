using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> AplicarFiltroDataOS(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            // Filtro nas OS
            if (parameters.PeriodoMediaAtendInicio != DateTime.MinValue && parameters.PeriodoMediaAtendInicio != DateTime.MinValue)
            {
                query = query
                   .Include(t => t.OrdensServico.Where(os => os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                   os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim));
            }

            return query;
        }
        
        public IQueryable<DashboardTecnicoDisponibilidadeTecnicoViewModel> AplicarFiltroDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            DateTime agora = DateTime.Now;

            // Filtro nas OS
            if (parameters.PeriodoMediaAtendInicio != DateTime.MinValue && parameters.PeriodoMediaAtendInicio != DateTime.MinValue)
            {
                query = query
                   .Include(t => t.OrdensServico.Where(os => os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                   os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim));
            }

            try
            {
                var t = (from tecnico in query.Where(q => q.IndAtivo == 1 && q.Usuario != null && q.Filial.NomeFilial == "FAM")//.ToArray()

                         //join ponto in _context.PontoUsuario.Where(p =>
                         //    p.DataHoraRegistro >= parameters.PeriodoMediaAtendInicio &&
                         //    p.DataHoraRegistro <= parameters.PeriodoMediaAtendFim)
                         //on tecnico.Usuario.CodUsuario equals ponto.CodUsuario into pontoUsuario

                         //join loc in _context.Localizacao.Where(w =>
                         //        w.DataHoraCad.AddMinutes(-90) >= agora &&
                         //        w.DataHoraCad.AddMinutes(90) <= agora
                         //        ) on tecnico.Usuario.CodUsuario equals loc.CodUsuario into temp
                         //from local in temp.DefaultIfEmpty()

                         select new DashboardTecnicoDisponibilidadeTecnicoViewModel()
                         {
                             //NomeUsuario = tecnico.Usuario.NomeUsuario,
                             //CodUsuario = tecnico.Usuario.CodUsuario,
                             CodTecnico = tecnico.CodTecnico,
                             Filial = tecnico.Filial,
                             IndFerias = tecnico.IndFerias,
                             //IndUsuarioAtivo = tecnico.Usuario.IndAtivo,
                             //IndTecnicoAtivo = tecnico.IndAtivo.Value,
                             //QtdPonto = pontoUsuario.Count(),
                             //SinalSatelite = local != null ? local.CodLocalizacao : null,

                             //QtdChamadosTotal = tecnico.OrdensServico.Count,

                             //QtdChamadosTransferidos = tecnico.OrdensServico.Where(t => t.CodStatusServico != (int)StatusServicoEnum.TRANSFERIDO).Count(),

                             QtdChamadosAtendidosTodasIntervencoes = tecnico.OrdensServico.Where(os =>
                                                os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                                os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                                                os.CodTipoIntervencao != (int)TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO &&
                                                os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELPDESK &&
                                                os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELP_DESK_DSS
                                               ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                             rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                             rat.DataHoraSolucao > agora.AddDays(-30)),

                             QtdChamadosAtendidosSomenteCorretivos = tecnico.OrdensServico.Where(os =>
                             os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                                os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                                                os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                                               ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                             rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                             rat.DataHoraSolucao > agora.AddDays(-30)),

                             //QtdChamadosAtendidosSomenteInstalacao = tecnico.OrdensServico.Where(os =>
                             //os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                             //                   os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                             //                   os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO
                             //                  ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                             //                rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                             //                rat.DataHoraSolucao > agora.AddDays(-30)),

                             //QtdChamadosAtendidosSomenteEngenharia = tecnico.OrdensServico.Where(os =>
                             //os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                             //                   os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                             //                   os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ALTERACAO_DE_ENGENHARIA
                             //                  ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                             //                rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                             //                rat.DataHoraSolucao > agora.AddDays(-30)),

                             QtdChamadosAtendidosSomentePreventivos = tecnico.OrdensServico.Where(os =>
                             os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                                os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                                                 os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                                               ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                             rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                             rat.DataHoraSolucao > agora.AddDays(-30)),

                             QtdChamadosAtendidosTodasIntervencoesDia = tecnico.OrdensServico.Where(os =>
                             os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                                os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim).SelectMany(r => r.RelatoriosAtendimento)
                                                                      .Count(rat =>
                                                                     rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                                                     rat.DataHoraSolucao >= agora.Date),

                             //QtdDiasUteisTech = (from p in pontoUsuario.Where(d => d.DataHoraRegistro >= agora.AddDays(-30))
                             //                    where
                             //                    !_context.PlantaoTecnico
                             //                    .Where(s => s.CodUsuarioCad == p.CodUsuario)
                             //                    .Select(s => s.DataPlantao)
                             //                    .Contains(p.DataHoraRegistro)
                             //                    select p.DataHoraRegistro).Distinct().Count()

                         });//.ToList();


                //var ia = i.Any(s => s.QtdChamadosTotal > 0);

                return t;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
