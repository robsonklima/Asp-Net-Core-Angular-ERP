using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaAdiantamentoRepository : IDespesaAdiantamentoRepository
    {
        private readonly AppDbContext _context;

        public DespesaAdiantamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaAdiantamento despesa)
        {
            _context.ChangeTracker.Clear();
            DespesaAdiantamento d = _context.DespesaAdiantamento.FirstOrDefault(l => l.CodDespesaAdiantamento == despesa.CodDespesaAdiantamento);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(despesa);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(DespesaAdiantamento despesa)
        {
            _context.Add(despesa);
            _context.SaveChanges();
        }

        public DespesaAdiantamentoSolicitacao CriarSolicitacao(DespesaAdiantamentoSolicitacao solicitacao)
        {
            _context.Add(solicitacao);
            _context.SaveChanges();
            return solicitacao;
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public List<ViewMediaDespesasAdiantamento> ObterMediaAdiantamentos(int codTecnico)
        {
            return _context.ViewMediaDespesasAdiantamento.Where(m => m.CodTecnico == codTecnico).ToList();
        }

        public DespesaAdiantamento ObterPorCodigo(int codigo) =>
            _context.DespesaAdiantamento
                .FirstOrDefault(d => d.CodDespesaAdiantamento == codigo);

        public PagedList<DespesaAdiantamento> ObterPorParametros(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _context.DespesaAdiantamento
            .Include(da => da.DespesaAdiantamentoTipo)
            .Include(da => da.Tecnico)
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var codigos = parameters.CodTecnicos.Split(",").Select(a => a.Trim());
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => codigos.Any(p => p == e.CodTecnico.ToString()));
            }

            if (parameters.IndAtivo.HasValue)
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => e.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.CodDespesaAdiantamentoTipo))
            {
                var tipos = parameters.CodDespesaAdiantamentoTipo.Split(",").Select(a => a.Trim());
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => tipos.Any(p => p == e.CodDespesaAdiantamentoTipo.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaAdiantamento = despesaAdiantamento.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaAdiantamento>.ToPagedList(despesaAdiantamento, parameters.PageNumber, parameters.PageSize);
        }
    }
}