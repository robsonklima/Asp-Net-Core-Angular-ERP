using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ContratoReajusteRepository : IContratoReajusteRepository
    {
        private readonly AppDbContext _context;

        public ContratoReajusteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoReajuste contratoReajuste)
        {
            _context.ChangeTracker.Clear();
            ContratoReajuste tc = _context.ContratoReajuste.FirstOrDefault(tc => tc.CodContrato == contratoReajuste.CodContrato);
            
            if (tc != null)
            {
                contratoReajuste.CodContratoReajuste = tc.CodContratoReajuste;        
                _context.Entry(tc).CurrentValues.SetValues(contratoReajuste);
                _context.SaveChanges();
            }
        }

        public void Criar(ContratoReajuste tipoCausa)
        {
            _context.Add(tipoCausa);
            _context.SaveChanges();
        }

        public void Deletar(int codContratoReajuste)
        {
            ContratoReajuste tc = _context.ContratoReajuste.FirstOrDefault(tc => tc.CodContratoReajuste == codContratoReajuste);

            if (tc != null)
            {
                _context.ContratoReajuste.Remove(tc);
                _context.SaveChanges();
            }
        }

        public ContratoReajuste ObterPorCodigo(int codigo)
        {
            return _context.ContratoReajuste.FirstOrDefault(tc => tc.CodContratoReajuste == codigo);
        }

        public PagedList<ContratoReajuste> ObterPorParametros(ContratoReajusteParameters parameters)
        {
            var tipos = _context.ContratoReajuste.AsQueryable();

            if (parameters.CodTipoIndiceReajuste != null)
            {
                tipos = tipos.Where(t => t.CodTipoIndiceReajuste == parameters.CodTipoIndiceReajuste );
            }

            if (parameters.CodContrato != null)
            {
                tipos = tipos.Where( t => t.CodContrato == parameters.CodContrato );
            }

            if (parameters.CodContratoReajuste != null)
            {
                tipos = tipos.Where(t => t.CodContratoReajuste == parameters.CodContratoReajuste );
            }

             if (parameters.IndAtivo != null)
            {
                tipos = tipos.Where(t => t.IndAtivo == parameters.IndAtivo );
            }

            return PagedList<ContratoReajuste>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
