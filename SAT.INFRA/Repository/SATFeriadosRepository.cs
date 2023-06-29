using SAT.INFRA.Context;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.UTILS;

namespace SAT.INFRA.Repository
{
    public class SATFeriadosRepository : ISATFeriadosRepository
    {
        private readonly AppDbContext _context;

        public SATFeriadosRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(SATFeriados satFeriados)
        {
            _context.ChangeTracker.Clear();
            SATFeriados f = _context.SATFeriados.FirstOrDefault(f => f.CodSATFeriados == satFeriados.CodSATFeriados);

            if (f != null)
            {
                _context.Entry(f).CurrentValues.SetValues(satFeriados);
                _context.SaveChanges();
            }
        }

        public void Criar(SATFeriados satFeriados)
        {
            _context.Add(satFeriados);
            _context.SaveChanges();
        }

        public void Deletar(int codSATFeriados)
        {
            SATFeriados f = _context.SATFeriados.FirstOrDefault(f => f.CodSATFeriados == codSATFeriados);

            if (f != null)
            {
                _context.SATFeriados.Remove(f);
                _context.SaveChanges();
            }
        }

        public SATFeriados ObterPorCodigo(int codigo)
        {
            return _context.SATFeriados.FirstOrDefault(f => f.CodSATFeriados == codigo);
        }

        public PagedList<SATFeriados> ObterPorParametros(SATFeriadosParameters parameters)
        {
            var satFeriadoss = _context.SATFeriados
                .AsQueryable();

            if (parameters.Filter != null)
            {
                satFeriadoss = satFeriadoss.Where(
                    f =>
                    f.CodSATFeriados.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty));
            }

            if (parameters.CodSATFeriados != null)
            {
                satFeriadoss = satFeriadoss.Where(f => f.CodSATFeriados == parameters.CodSATFeriados);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Tipo))
                satFeriadoss = satFeriadoss.Where(f => f.Tipo.Contains(parameters.Tipo));

            if (!string.IsNullOrWhiteSpace(parameters.UF))
                satFeriadoss = satFeriadoss.Where(f => f.UF.Contains(parameters.UF));       

            if (parameters.Mes.HasValue)
                satFeriadoss = satFeriadoss.Where(f => DataHelper.ConverterStringParaData(f.Data).Month == parameters.Mes.Value);         
            
            if (!string.IsNullOrWhiteSpace(parameters.Municipio))
                satFeriadoss = satFeriadoss.Where(f => f.Municipio.Contains(parameters.Municipio));

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                satFeriadoss = satFeriadoss.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<SATFeriados>.ToPagedList(satFeriadoss, parameters.PageNumber, parameters.PageSize);
        }
    }
}
