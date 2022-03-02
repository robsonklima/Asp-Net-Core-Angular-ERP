using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace SAT.INFRA.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _context;

        public CargoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Cargo cargo)
        {
            _context.ChangeTracker.Clear();
            Cargo c = _context.Cargo.FirstOrDefault(c => c.CodCargo == cargo.CodCargo);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(cargo);
                _context.SaveChanges();
            }
        }

        public Cargo BuscaCargoPorNome(string nomeCargo)
        {
            Cargo[] listaCargos = _context.Cargo
                .Where(c => c.NomeCargo.StartsWith(nomeCargo.Substring(0, 1))).ToArray();

            Cargo retorno = null;
            foreach (Cargo Cargo in listaCargos)
            {
                string regex = Regex.Replace(Cargo.NomeCargo, "[^a-zA-Z]+", "").ToLower();

                if (regex == nomeCargo)
                {
                    retorno = Cargo;
                    break;
                }
            }

            return retorno;
        }

        public void Criar(Cargo Cargo)
        {
            _context.Add(Cargo);
            _context.SaveChanges();
        }

        public void Deletar(int codCargo)
        {
            Cargo c = _context.Cargo.FirstOrDefault(c => c.CodCargo == codCargo);

            if (c != null)
            {
                _context.Cargo.Remove(c);
                _context.SaveChanges();
            }
        }

        public Cargo ObterPorCodigo(int codigo)
        {
            return _context.Cargo
                .FirstOrDefault(c => c.CodCargo == codigo);
        }

        public PagedList<Cargo> ObterPorParametros(CargoParameters parameters)
        {
            IQueryable<Cargo> query = this.ObterQuery(parameters);

            return PagedList<Cargo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public IQueryable<Cargo> ObterQuery(CargoParameters parameters)
        {
            IQueryable<Cargo> query = _context.Cargo.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    s =>
                    s.CodCargo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeCargo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (parameters.CodCargo.HasValue)
                query = query.Where(c => c.CodCargo == parameters.CodCargo);

            if (parameters.IndAtivo.HasValue)
                query = query.Where(c => c.IndAtivo == parameters.IndAtivo);

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return query.AsNoTracking();
        }

    }
}
