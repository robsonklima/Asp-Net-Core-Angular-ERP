using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class CheckinCheckoutRepository : ICheckinCheckoutRepository
    {
        private readonly AppDbContext _context;

        public CheckinCheckoutRepository(AppDbContext context)
        {
            _context = context;
        }   

        public PagedList<CheckinCheckout> ObterPorParametros(CheckinCheckoutParameters parameters)
        {
            var checkinCheckout = _context.CheckinCheckout
            .AsQueryable();
            
            if (parameters.CodOS.HasValue)
                checkinCheckout = checkinCheckout.Where(c => c.CodOS == parameters.CodOS);

            if (parameters.CodRAT.HasValue)
                checkinCheckout = checkinCheckout.Where(c => c.CodRAT == parameters.CodRAT);

            if (parameters.CodUsuarioTecnico != null)
                checkinCheckout = checkinCheckout.Where(c => c.CodUsuarioTecnico == parameters.CodUsuarioTecnico);                

            return PagedList<CheckinCheckout>.ToPagedList(checkinCheckout, parameters.PageNumber, parameters.PageSize);
        }
    }
}
