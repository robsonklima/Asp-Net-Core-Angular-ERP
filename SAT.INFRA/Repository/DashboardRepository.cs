using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using System;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Defeito defeito)
        {
            Defeito d = _context.Defeito.FirstOrDefault(d => d.CodDefeito == defeito.CodDefeito);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(defeito);
                _context.SaveChanges();
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
                    retorno.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Indicador>>(dash.DadosJson));
                }
            }

            return retorno;
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim)
        {
            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> retorno = new();

            List<DashboardIndicadores> indicador = this._context.DashboardIndicadores
                .Where(f => f.NomeIndicador == nomeIndicador &&
                f.Data >= dataInicio.Value.Date &&
                f.Data <= dataFim.Value.Date).ToList();

            if (indicador.Any())
            {
                foreach (DashboardIndicadores dash in indicador)
                {
                    retorno.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<DashboardTecnicoDisponibilidadeTecnicoViewModel>>(dash.DadosJson));
                }
            }

            return retorno;
        }
    }
}
