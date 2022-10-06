using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ProtocoloSTNRepository : IProtocoloSTNRepository
    {
        private readonly AppDbContext _context;

        public ProtocoloSTNRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ProtocoloSTN protocoloSTN)
        {
            _context.ChangeTracker.Clear();
            ProtocoloSTN per = _context.ProtocoloSTN.SingleOrDefault(p => p.CodProtocoloSTN == protocoloSTN.CodProtocoloSTN);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(protocoloSTN);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public ProtocoloSTN Criar(ProtocoloSTN protocoloSTN)
        {
            try
            {
                _context.Add(protocoloSTN);
                _context.SaveChanges();
                return protocoloSTN;
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            ProtocoloSTN per = _context.ProtocoloSTN.SingleOrDefault(p => p.CodProtocoloSTN == codigo);

            if (per != null)
            {
                _context.ProtocoloSTN.Remove(per);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public ProtocoloSTN ObterPorCodigo(int codigo)
        {
            return _context.ProtocoloSTN.SingleOrDefault(p => p.CodProtocoloSTN == codigo);
        }

        public PagedList<ProtocoloSTN> ObterPorParametros(ProtocoloSTNParameters parameters)
        {
            var query = _context.ProtocoloSTN
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodProtocoloSTN.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ProtocoloSTN>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
