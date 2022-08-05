using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ContratoServicoRepository : IContratoServicoRepository
    {
        private readonly AppDbContext _context;

        public ContratoServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoServico contratoServico)
        {
            _context.ChangeTracker.Clear();
            ContratoServico ce = _context.ContratoServico
                                                .FirstOrDefault(ce => ce.CodContrato == contratoServico.CodContrato
                                                                        && ce.CodServico == contratoServico.CodServico);
            try
            {
                if (ce != null)
                {
                    _context.Entry(ce).CurrentValues.SetValues(contratoServico);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ContratoServico contratoServico)
        {
            try
            {
                _context.Add(contratoServico);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codContrato, int codContratoServico)
        {
            ContratoServico ce = _context.ContratoServico
                                            .FirstOrDefault(d => d.CodContrato == codContrato && d.CodContratoServico == codContratoServico);

            if (ce != null)
            {
                _context.ContratoServico.Remove(ce);
                _context.SaveChanges();
            }
        }

        public ContratoServico ObterPorCodigo(int codContrato, int codContratoServico)
        {
            try
            {
                return _context.ContratoServico
                                    .SingleOrDefault(ce => ce.CodContrato == codContrato
                                                        && ce.CodContratoServico == codContratoServico);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<ContratoServico> ObterPorParametros(ContratoServicoParameters parameters)
        {
            var contratoServico = _context.ContratoServico
                .Include(c => c.Contrato)
                .AsQueryable();

            if (parameters.CodContrato != null)
            {
                contratoServico = contratoServico.Where(a => a.CodContrato == parameters.CodContrato);
            }


            return PagedList<ContratoServico>.ToPagedList(contratoServico, parameters.PageNumber, parameters.PageSize);
        }
    }
}
