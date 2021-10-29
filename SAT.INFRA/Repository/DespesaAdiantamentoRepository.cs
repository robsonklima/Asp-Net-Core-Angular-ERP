using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
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
            throw new NotImplementedException();
        }

        public void Criar(DespesaAdiantamento despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaAdiantamento ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaAdiantamento> ObterPorParametros(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _context.DespesaAdiantamento
            .Include(da => da.DespesaAdiantamentoTipo)
            .Include(da => da.Tecnico)
            .AsQueryable();

            return PagedList<DespesaAdiantamento>.ToPagedList(despesaAdiantamento, parameters.PageNumber, parameters.PageSize);
        }
    }
}