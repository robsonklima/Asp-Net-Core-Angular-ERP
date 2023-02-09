using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Views;
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
                .Include(t => t.Cidade)
                .Include(t => t.Cidade.UnidadeFederativa)
                .Include(t => t.DespesaCartaoCombustivelTecnico)
                     .ThenInclude(t => t.DespesaCartaoCombustivel)
                .Include(t => t.TecnicoContas)
                .Include(t => t.Veiculos.OrderByDescending(v => v.CodVeiculoCombustivel))
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
            var tecnicos = _context.Tecnico.AsNoTracking().AsQueryable();

            tecnicos = AplicarIncludes(tecnicos, parameters.Include);
            tecnicos = AplicarFiltros(tecnicos, parameters);
            tecnicos = AplicarOrdenacao(tecnicos, parameters.SortActive, parameters.SortDirection);

            return PagedList<Tecnico>.ToPagedList(tecnicos, parameters.PageNumber, parameters.PageSize);
        }

        public List<ViewTecnicoTempoAtendimento> ObterTempoAtendimento(int codTecnico)
        {
            var query = _context.ViewTecnicoTempoAtendimento.AsQueryable();

            if (codTecnico > 0)
                query = query.Where(t => t.CodTecnico == codTecnico);

            return query.ToList();
        }

        public PagedList<ViewTecnicoDeslocamento> ObterDeslocamentos(TecnicoParameters parameters)
        {
            var query = _context.ViewTecnicoDeslocamento.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter)) {
                query = query.Where(t => t.Tecnico.Contains(parameters.Filter) || t.Filial.Contains(parameters.Filter));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] filiais = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => filiais.Contains(t.CodFilial));
            }

            if (!string.IsNullOrEmpty(parameters.CodRegioes))
            {
                int[] regioes = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => regioes.Contains(t.CodRegiao));
            }

            if (!string.IsNullOrEmpty(parameters.CodAutorizadas))
            {
                int[] autorizadas = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => autorizadas.Contains(t.CodAutorizada));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ViewTecnicoDeslocamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
