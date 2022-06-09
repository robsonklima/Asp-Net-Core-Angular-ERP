using SAT.MODELS.Entities;
using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IFaturamentoFinanceiroService
    {
        Task<FaturamentoFinanceiro> Criar(FaturamentoFinanceiro faturamento);
    }
}
