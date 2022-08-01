using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        private readonly AppDbContext _context;
        public TecnicoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Atualizar(Tecnico tecnico)
        {
            _context.ChangeTracker.Clear();
            Tecnico t = _context.Tecnico.FirstOrDefault(t => t.CodTecnico == tecnico.CodTecnico);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(tecnico);
                _context.SaveChanges();
            }
        }

        public void Criar(Tecnico tecnico)
        {
            _context.Add(tecnico);
            _context.SaveChanges();
        }

        public void Deletar(int codTecnico)
        {
            Tecnico t = _context.Tecnico.FirstOrDefault(t => t.CodTecnico == codTecnico);

            if (t != null)
            {
                _context.Tecnico.Remove(t);
                _context.SaveChanges();
            }
        }

        public Tecnico ObterPorCodigo(int codigo)
        {
            return _context.Tecnico
                .Include(t => t.Filial)
                .Include(t => t.Autorizada)
                .Include(t => t.TipoRota)
                .Include(t => t.Regiao)
                .Include(t => t.Usuario)
                    .ThenInclude(t => t.Localizacoes)
                .Include(t => t.Cidade)
                .Include(t => t.Cidade.UnidadeFederativa)
                .Include(t => t.DespesaCartaoCombustivelTecnico)
                     .ThenInclude(t => t.DespesaCartaoCombustivel)
                .Include(t => t.TecnicoContas)
                .FirstOrDefault(t => t.CodTecnico == codigo);
        }

        public IQueryable<Tecnico> ObterQuery(TecnicoParameters parameters)
        {
            IQueryable<Tecnico> query = _context.Tecnico.AsQueryable();

            query = AplicarIncludes(query, parameters.Include);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query.AsNoTracking();
        }

        public PagedList<Tecnico> ObterPorParametros(TecnicoParameters parameters)
        {
            var tecnicos = this.ObterQuery(parameters)
                .Include(t => t.Filial)
                .Include(t => t.Autorizada)
                .Include(t => t.Regiao)
                .AsQueryable();

             if (parameters.Filter != null)
            {
                tecnicos = tecnicos.Where(
                            t =>
                            t.CodTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

           if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                tecnicos = tecnicos.Where(dc => cods.Contains(dc.CodFilial.Value));

            }

             if (!string.IsNullOrWhiteSpace(parameters.CodAutorizadas))
            {
                int[] cods = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                tecnicos = tecnicos.Where(dc => cods.Contains(dc.CodAutorizada.Value));
            }

             if (!string.IsNullOrWhiteSpace(parameters.CodRegioes))
            {
                int[] cods = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                tecnicos = tecnicos.Where(dc => cods.Contains(dc.CodRegiao.Value));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tecnicos = tecnicos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (parameters.IndAtivo.HasValue)
                tecnicos = tecnicos.Where(e => e.IndAtivo == parameters.IndAtivo);
            return PagedList<Tecnico>.ToPagedList(tecnicos, parameters.PageNumber, parameters.PageSize);
        }

        public List<ViewTecnicoTempoAtendimento> ObterTempoAtendimento(int codTecnico)
        {
            var query = _context.ViewTecnicoTempoAtendimento.AsQueryable();

            if (codTecnico > 0)
                query = query.Where(t => t.CodTecnico == codTecnico);

            return query.ToList();
        }
    }
}
