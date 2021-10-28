using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroAgendaTecnico(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                query = query.Where(os => filiais.Any(p => p == os.CodFilial.ToString()));
            }

            query = query.Where(os =>
                // (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO) && 
                (os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO ||
                ((os.CodStatusServico == (int)StatusServicoEnum.ABERTO || os.CodStatusServico == (int)StatusServicoEnum.FECHADO) && os.DataHoraTransf.Value.Date == DateTime.Now.Date)));

            return query;
        }
    }
}
