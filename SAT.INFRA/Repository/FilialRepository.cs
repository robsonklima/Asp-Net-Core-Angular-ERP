using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public FilialRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(Filial filial)
        {
            try
            {
                _context.ChangeTracker.Clear();
                Filial linha = _context.Filial.FirstOrDefault(c => c.CodFilial == filial.CodFilial);

                if (linha != null)
                {
                    _context.Entry(linha).CurrentValues.SetValues(filial);
                    _context.Entry(linha).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Criar(Filial filial)
        {
            filial.CodFilial = this._sequenciaRepository.ObterContador("Filial");
            _context.Add(filial);
            _context.SaveChanges();
        }

        public void Deletar(int codFilial)
        {
            Filial linha = _context.Filial.FirstOrDefault(c => c.CodFilial == codFilial);

            if (linha != null)
            {
                _context.Filial.Remove(linha);
                _context.SaveChanges();
            }
        }

        public Filial ObterPorCodigo(int codigo)
        {
            return _context.Filial
                .Include(i => i.Cidade)
                    .ThenInclude(i => i.UnidadeFederativa)
                         .ThenInclude(i => i.Pais)
                .FirstOrDefault(f => f.CodFilial == codigo);
        }

        public PagedList<Filial> ObterPorParametros(FilialParameters parameters)
        {
            var query = _context.Filial.AsNoTracking().AsQueryable();

            // query = AplicarIncludes(query, parameters.Include);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return PagedList<Filial>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
