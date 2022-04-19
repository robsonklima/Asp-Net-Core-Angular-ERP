using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public partial class ClientePecaGenericaRepository : IClientePecaGenericaRepository
    {
        private readonly AppDbContext _context;

        public ClientePecaGenericaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ClientePecaGenerica clientePecaGenerica)
        {
            _context.ChangeTracker.Clear();
            ClientePecaGenerica p = _context.ClientePecaGenerica.FirstOrDefault(p => p.CodClientePecaGenerica == clientePecaGenerica.CodClientePecaGenerica);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(clientePecaGenerica);
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(ClientePecaGenerica clientePecaGenerica)
        {
            try
            {
                _context.Add(clientePecaGenerica);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codClientePecaGenerica)
        {
            ClientePecaGenerica p = _context.ClientePecaGenerica.FirstOrDefault(p => p.CodClientePecaGenerica == codClientePecaGenerica);

            if (p != null)
            {
                _context.ClientePecaGenerica.Remove(p);
                _context.SaveChanges();
            }
        }

        //Peca já tem lista de ClientePeca, acontece erro se usar include direto
        private IQueryable<ClientePecaGenerica> MontaClientePecaQuery()
        {
            return (from c in _context.ClientePecaGenerica
                    join p in _context.Peca on c.CodPeca equals p.CodPeca into joinPeca
                    from peca in joinPeca.DefaultIfEmpty()
                    select new ClientePecaGenerica
                    {
                        CodClientePecaGenerica = c.CodClientePecaGenerica,
                        CodPeca = c.CodPeca,
                        CodUsuarioCad = c.CodUsuarioCad,
                        CodUsuarioManut = c.CodUsuarioManut,
                        DataHoraCad = c.DataHoraCad,
                        DataHoraManut = c.DataHoraManut,
                        ValorIPI = c.ValorIPI,
                        ValorUnitario = c.ValorUnitario,
                        VlrBaseTroca = c.VlrBaseTroca,
                        VlrSubstituicaoNovo = c.VlrSubstituicaoNovo,
                        Peca = peca
                    })
                    .AsQueryable();
        }

        public ClientePecaGenerica ObterPorCodigo(int codigo)
        {
            return this.MontaClientePecaQuery().FirstOrDefault(p => p.CodClientePecaGenerica == codigo);
        }

        public IQueryable<ClientePecaGenerica> ObterQuery(ClientePecaGenericaParameters parameters)
        {
            var query = this.MontaClientePecaQuery();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    c =>
                   c.Peca.NomePeca.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodPeca.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodClientePecaGenerica.HasValue)
            {
                query = query.Where(a => a.CodClientePecaGenerica == parameters.CodClientePecaGenerica);
            }

            if (parameters.CodMagnus != null)
            {
                query = query.Where(a => a.Peca.CodMagnus == parameters.CodMagnus);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return query;
        }

        public PagedList<ClientePecaGenerica> ObterPorParametros(ClientePecaGenericaParameters parameters)
        {
            var query = this.ObterQuery(parameters);

            return PagedList<ClientePecaGenerica>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}