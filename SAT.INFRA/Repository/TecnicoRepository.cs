using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly AppDbContext _context;

        public TecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Tecnico tecnico)
        {
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
                .FirstOrDefault(t => t.CodTecnico == codigo);
        }

        public PagedList<Tecnico> ObterPorParametros(TecnicoParameters parameters)
        {
            var tecnicos = _context.Tecnico
                .Include(t => t.Filial)
                .Include(t => t.Autorizada)
                .Include(t => t.TipoRota)
                .Include(t => t.Regiao)
                .Include(t => t.Usuario)
                .Include(t => t.RegiaoAutorizada)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                tecnicos = tecnicos.Where(
                    t =>
                    t.CodTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Regiao.NomeRegiao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Autorizada.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.IndAtivo != null)
            {
                tecnicos = tecnicos.Where(t => t.IndAtivo == parameters.IndAtivo); // && t.Usuario.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.IndFerias != null)
            {
                tecnicos = tecnicos.Where(t => t.IndFerias == parameters.IndFerias);
            }

            if (parameters.CodTecnico != null)
            {
                tecnicos = tecnicos.Where(t => t.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.CodPerfil != null)
            {
                tecnicos = tecnicos.Where(t => t.Usuario.CodPerfil == parameters.CodPerfil);
            }

            if (parameters.Nome != null)
            {
                tecnicos = tecnicos.Where(t => t.Nome == parameters.Nome || t.Nome.Contains(parameters.Nome));
            }

            if (parameters.CodAutorizada != null)
            {
                tecnicos = tecnicos.Where(t => t.CodAutorizada == parameters.CodAutorizada);
            }

            if (parameters.PA != null)
            {
                tecnicos = tecnicos.Where(t => t.RegiaoAutorizada.PA == parameters.PA);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tecnicos = tecnicos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(",");
                tecnicos = tecnicos.Where(t => filiais.Any(a => a == t.CodFilial.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var tecs = parameters.CodTecnicos.Split(",");
                tecnicos = tecnicos.Where(t => tecs.Any(a => a == t.CodTecnico.ToString()));
            }

            if (parameters.CodStatusServicos != null)
            {
                var codigosStatusServico = parameters.CodStatusServicos.Split(',');

                tecnicos = tecnicos
                    .Include(t => t.OrdensServico.Where(os => codigosStatusServico.Contains(os.CodStatusServico.ToString())))
                        .ThenInclude(os => os.TipoIntervencao);
            }

            return PagedList<Tecnico>.ToPagedList(tecnicos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
