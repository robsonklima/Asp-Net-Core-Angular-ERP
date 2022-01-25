using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using System;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SAT.INFRA.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(string nomeIndicador, List<Indicador> indicadores, DateTime data)
        {
            _context.ChangeTracker.Clear();
            DashboardIndicadores indicador = _context.DashboardIndicadores.FirstOrDefault(f => f.NomeIndicador == nomeIndicador && f.Data == data.Date);

            if (indicador != null)
            {
                DashboardIndicadores indicadorAtualizado = indicador;
                indicadorAtualizado.DadosJson = Newtonsoft.Json.JsonConvert.SerializeObject(indicadores);
                indicadorAtualizado.UltimaAtualizacao = DateTime.Now;
                _context.Entry(indicador).CurrentValues.SetValues(indicadorAtualizado);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                this.Criar(nomeIndicador, indicadores, data);
            }
        }

        public void Atualizar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data)
        {
            _context.ChangeTracker.Clear();
            DashboardIndicadores indicador = _context.DashboardIndicadores.FirstOrDefault(f => f.NomeIndicador == nomeIndicador && f.Data == data.Date);

            if (indicador != null)
            {
                DashboardIndicadores indicadorAtualizado = indicador;
                indicadorAtualizado.DadosJson = Newtonsoft.Json.JsonConvert.SerializeObject(indicadores);
                indicadorAtualizado.UltimaAtualizacao = DateTime.Now;
                try
                {
                    _context.Entry(indicador).CurrentValues.SetValues(indicadorAtualizado);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                this.Criar(nomeIndicador, indicadores, data);
            }
        }

        public void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data)
        {
            DashboardIndicadores novoDado = new()
            {
                DadosJson = Newtonsoft.Json.JsonConvert.SerializeObject(indicadores),
                NomeIndicador = nomeIndicador,
                Data = data,
                UltimaAtualizacao = DateTime.Now
            };

            _context.Add(novoDado);
            _context.SaveChanges();
        }

        public void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data)
        {
            DashboardIndicadores novoDado = new()
            {
                DadosJson = Newtonsoft.Json.JsonConvert.SerializeObject(indicadores),
                NomeIndicador = nomeIndicador,
                Data = data,
                UltimaAtualizacao = DateTime.Now
            };

            _context.Add(novoDado);
            _context.SaveChanges();
        }

        public Defeito ObterPorCodigo(int codigo)
        {
            return _context.Defeito.FirstOrDefault(d => d.CodDefeito == codigo);
        }

        public PagedList<Defeito> ObterPorParametros(DefeitoParameters parameters)
        {
            var defeitos = _context.Defeito.AsQueryable();

            if (parameters.Filter != null)
            {
                defeitos = defeitos.Where(
                    s =>
                    s.CodDefeito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.CodEDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodDefeito != null)
            {
                defeitos = defeitos.Where(c => c.CodDefeito == parameters.CodDefeito);
            }

            if (parameters.IndAtivo != null)
            {
                defeitos = defeitos.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                defeitos = defeitos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Defeito>.ToPagedList(defeitos, parameters.PageNumber, parameters.PageSize);
        }

        public PagedList<DashboardDisponibilidade> ObterPorParametros(DashboardParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public List<Indicador> ObterDadosIndicador(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim)
        {
            List<Indicador> retorno = new();

            List<DashboardIndicadores> indicador = this._context.DashboardIndicadores
                .Where(f => f.NomeIndicador == nomeIndicador &&
                f.Data >= dataInicio.Value.Date &&
                f.Data <= dataFim.Value.Date).ToList();

            if (indicador.Any())
            {
                foreach (DashboardIndicadores dash in indicador)
                {
                    retorno.AddRange(JsonConvert.DeserializeObject<List<Indicador>>(dash.DadosJson));
                }
            }

            return retorno;
        }

        public List<Indicador> ObterDadosIndicadorMaisRecente(string nomeIndicador)
        {
            List<Indicador> retorno = new();
            DateTime ultimoDia = DateTime.Now;
            while (retorno.Count == 0)
            {
                DashboardIndicadores indicador =
                    this._context.DashboardIndicadores
                    .Where(f => f.NomeIndicador == nomeIndicador && f.Data <= ultimoDia)
                    .OrderByDescending(ord => ord.Data)
                    .FirstOrDefault();

                if (indicador != null)
                {
                    retorno.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Indicador>>(indicador.DadosJson));
                }

                ultimoDia = ultimoDia.AddDays(-1);
            }

            return retorno;
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime data)
        {
            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> retorno = new();

            DashboardIndicadores indicador = this._context.DashboardIndicadores
                .FirstOrDefault(f => f.NomeIndicador == nomeIndicador && f.Data == data.Date);

            if (indicador == null)
            {
                indicador = this._context.DashboardIndicadores
                   .Where(f => f.NomeIndicador == nomeIndicador)
                   .OrderByDescending(ord => ord.Data)
                   .FirstOrDefault();
            }

            retorno.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<DashboardTecnicoDisponibilidadeTecnicoViewModel>>(indicador.DadosJson));

            return retorno;
        }

        /// <summary>
        /// Faz o calculo do dashboard dos técnicos nesta camada pois usa o _context
        /// </summary>
        /// <param name="query">Lista dos técnicos</param>
        /// <param name="parameters"></param>
        /// <param name="FuncDiasUteis">Função dos Feriados - Calcular dias uteis: serve para ver os dias uteis na range de pontos</param>
        /// <returns></returns>
        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters
           , Func<DateTime, DateTime, int> FuncDiasUteis)
        {
            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> retorno = new();

            Tecnico[] tecnicosAtivos = query.Where(q => q.IndAtivo == 1 && q.Usuario != null &&
                                                     q.Usuario.IndAtivo == 1 && q.Filial != null).ToArray();

            foreach (var tecnico in tecnicosAtivos)
            {
                IEnumerable<PontoUsuario> pontoUsuario = _context.PontoUsuario.Where(p =>
                       p.DataHoraRegistro >= parameters.PeriodoMediaAtendInicio &&
                       p.DataHoraRegistro <= parameters.PeriodoMediaAtendFim &&
                   tecnico.Usuario.CodUsuario == p.CodUsuario && p.IndAtivo == 1);

                // Se por algum motivo não tem ponto ou chamados, não tem porque contabilizar 
                if (pontoUsuario.Count() == 0 || tecnico.OrdensServico.Count == 0) continue;

                int diasTrabalhados = FuncDiasUteis(pontoUsuario.Min(s => s.DataHoraRegistro), pontoUsuario.Max(s => s.DataHoraRegistro));

                // Não se consegue calcular médias com 0 - não tem porque considerar este dia
                if (diasTrabalhados == 0) continue;

                IEnumerable<OrdemServico> osTecnico = tecnico.OrdensServico.Where(os =>
                                       os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                       os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                                       os.RelatoriosAtendimento != null && os.RelatoriosAtendimento.Count > 0);

                retorno.Add(new DashboardTecnicoDisponibilidadeTecnicoViewModel()
                {
                    IndFerias = tecnico.IndFerias,
                    IndAtivo = tecnico.IndAtivo,
                    CodTecnico = tecnico.CodTecnico.Value,
                    CodFilial = tecnico.Filial.CodFilial,
                    NomeFilial = tecnico.Filial.NomeFilial,

                    TecnicoSemChamadosTransferidos = !tecnico.OrdensServico.Any(w => w.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO),

                    MediaAtendimentosPorDiaTodasIntervencoes = osTecnico.Where(os =>
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO &&
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELPDESK &&
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELP_DESK_DSS
                                           ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                         rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                         rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaCorretivos = osTecnico.Where(os =>
                                          os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                                                    ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                                  rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                                  rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaPreventivos = osTecnico.Where(os =>
                                         os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaInstalacoes = osTecnico.Where(os =>
                                        os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaEngenharia = osTecnico.Where(os =>
                                        os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ALTERACAO_DE_ENGENHARIA
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,
                });
            }

            return retorno;
        }
    }
}
