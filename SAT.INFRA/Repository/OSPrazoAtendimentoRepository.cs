using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OSPrazoAtendimentoRepository : IOSPrazoAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public OSPrazoAtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public OSPrazoAtendimento Atualizar(OSPrazoAtendimento prazo)
        {
            _context.ChangeTracker.Clear();
            OSPrazoAtendimento p = _context.OSPrazoAtendimento.FirstOrDefault(p => p.CodOSPrazoAtendimento == prazo.CodOSPrazoAtendimento);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(prazo);
                _context.SaveChanges();
            }

            return p;
        }

        public OSPrazoAtendimento Criar(OSPrazoAtendimento prazo)
        {
            _context.Add(prazo);
            _context.SaveChanges();
            return prazo;
        }

        public OSPrazoAtendimento Deletar(int codOSPrazoAtendimento)
        {
            OSPrazoAtendimento prazo = _context.OSPrazoAtendimento.FirstOrDefault(p => p.CodOSPrazoAtendimento == codOSPrazoAtendimento);

            if (prazo != null)
            {
                _context.OSPrazoAtendimento.Remove(prazo);
                _context.SaveChanges();
            }

            return prazo;
        }

        public OSPrazoAtendimento ObterPorCodigo(int codigo)
        {
            return _context.OSPrazoAtendimento.FirstOrDefault(p => p.CodOSPrazoAtendimento == codigo);
        }

        public PagedList<OSPrazoAtendimento> ObterPorParametros(OSPrazoAtendimentoParameters parameters)
        {
            var OSPrazoAtendimentoes = _context.OSPrazoAtendimento.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                OSPrazoAtendimentoes = OSPrazoAtendimentoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OSPrazoAtendimento>.ToPagedList(OSPrazoAtendimentoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
