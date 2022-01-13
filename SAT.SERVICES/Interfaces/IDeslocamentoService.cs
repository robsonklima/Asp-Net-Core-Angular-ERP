using System.Threading.Tasks;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDeslocamentoService
    {
        Task<ListViewModel> ObterPorParametrosAsync(DeslocamentoParameters parameters);
    }
}
