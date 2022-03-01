using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        private readonly AppDbContext _context;
        private readonly IDespesaRepository _despesaRepository;

        public DespesaPeriodoTecnicoRepository(
            AppDbContext context,
            IDespesaRepository despesaRepository)
        {
            _context = context;
            _despesaRepository = despesaRepository;
        }

        public void Atualizar(DespesaPeriodoTecnico despesaTecnico)
        {
            _context.ChangeTracker.Clear();
            DespesaPeriodoTecnico d = _context.DespesaPeriodoTecnico.SingleOrDefault(l => l.CodDespesaPeriodoTecnico == despesaTecnico.CodDespesaPeriodoTecnico);

            if (d != null)
            {
                try
                {
                    _context.Entry(d).CurrentValues.SetValues(despesaTecnico);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(DespesaPeriodoTecnico despesa)
        {
            _context.Add(despesa);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaPeriodoTecnico ObterPorCodigo(int codigo)
        {
            return _context.DespesaPeriodoTecnico
                .Include(dpt => dpt.DespesaPeriodo)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                        .ThenInclude(dp => dp.DespesaItemAlerta)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                        .ThenInclude(dp => dp.CidadeOrigem)
                            .ThenInclude(dpt => dpt.UnidadeFederativa)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                        .ThenInclude(dp => dp.CidadeDestino)
                            .ThenInclude(dpt => dpt.UnidadeFederativa)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                        .ThenInclude(dpi => dpi.DespesaTipo)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.RelatorioAtendimento)
                .Include(dpt => dpt.DespesaPeriodoTecnicoStatus)
                .Include(dpt => dpt.Tecnico)
                    .ThenInclude(dpt => dpt.Filial)
                .Include(dpt => dpt.Tecnico)
                    .ThenInclude(dpt => dpt.TecnicoConta)
                .Include(dpt => dpt.Tecnico)
                    .ThenInclude(dpt => dpt.DespesaCartaoCombustivelTecnico)
                        .ThenInclude(dpt => dpt.DespesaCartaoCombustivel)
                            .ThenInclude(dpt => dpt.TicketLogUsuarioCartaoPlaca)
                .Include(dpt => dpt.DespesaProtocoloPeriodoTecnico)
                .Include(dpt => dpt.TicketLogPedidoCredito)
                .FirstOrDefault(i => i.CodDespesaPeriodoTecnico == codigo);
        }

        public IQueryable<DespesaPeriodoTecnico> ObterQuery(DespesaPeriodoTecnicoParameters parameters)
        {
            var query = _context.DespesaPeriodoTecnico.AsQueryable();

            query = AplicarIncludes(query);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query.AsNoTracking();
        }

        public PagedList<DespesaPeriodoTecnico> ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var query = ObterQuery(parameters);

            return PagedList<DespesaPeriodoTecnico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}