using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface ICheckinCheckoutRepository
    {
        PagedList<CheckinCheckout> ObterPorParametros(CheckinCheckoutParameters parameters);
    }
}
