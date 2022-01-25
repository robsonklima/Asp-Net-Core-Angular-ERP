using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class TransportadoraRepository : ITransportadoraRepository
    {
        private readonly AppDbContext _context;

        public TransportadoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Transportadora transportadora)
        {
            Transportadora t = _context.Transportadora.FirstOrDefault(t => t.CodTransportadora == transportadora.CodTransportadora);

            if (t != null)
            {
                _context.ChangeTracker.Clear();
                _context.Entry(t).CurrentValues.SetValues(transportadora);
                _context.SaveChanges();
            }
        }

        public void Criar(Transportadora transportadora)
        {
            _context.Add(transportadora);
            _context.SaveChanges();
        }

        public void Deletar(int codTransportadora)
        {
            Transportadora t = _context.Transportadora.FirstOrDefault(t => t.CodTransportadora == codTransportadora);

            if (t != null)
            {
                _context.Transportadora.Remove(t);
                _context.SaveChanges();
            }
        }

        public Transportadora ObterPorCodigo(int codigo)
        {
            return _context.Transportadora.FirstOrDefault(t => t.CodTransportadora == codigo);
        }

        public PagedList<Transportadora> ObterPorParametros(TransportadoraParameters parameters)
        {
            var transportadoras = _context.Transportadora
                .Include(t => t.Cidade)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                transportadoras = transportadoras.Where(
                            t =>
                            t.NomeTransportadora.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTransportadora.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTransportadora != null)
            {
                transportadoras = transportadoras.Where(t => t.CodTransportadora == parameters.CodTransportadora);
            }

            if (parameters.indAtivo != null)
            {
                transportadoras = transportadoras.Where(t => t.IndAtivo == parameters.indAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                transportadoras = transportadoras.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Transportadora>.ToPagedList(transportadoras, parameters.PageNumber, parameters.PageSize);
        }
    }
}
