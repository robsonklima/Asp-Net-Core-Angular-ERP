using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarOrdenacao(IQueryable<OrdemServico> query, string sortActive, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortActive) && !string.IsNullOrEmpty(sortDirection))
            {
                switch (sortActive)
                {
                    case "fimSLA":
                        query = sortDirection == "asc" ?
                            query.OrderBy(q => q.PrazosAtendimento.OrderByDescending(i => i.CodOSPrazoAtendimento).FirstOrDefault().DataHoraLimiteAtendimento) :
                            query.OrderByDescending(q => q.PrazosAtendimento.OrderByDescending(i => i.CodOSPrazoAtendimento).FirstOrDefault().DataHoraLimiteAtendimento);
                        break;

                    case "statusOS":
                        query = sortDirection == "asc" ?
                            query.OrderBy(q => q.StatusServico.Abrev) :
                            query.OrderByDescending(q => q.StatusServico.Abrev);
                        break;

                    case "nomeRegiao":
                        query = sortDirection == "asc" ?
                            query.OrderBy(q => q.EquipamentoContrato.Regiao.NomeRegiao) :
                            query.OrderByDescending(q => q.EquipamentoContrato.Regiao.NomeRegiao);
                        break;

                    case "pa":
                        query = sortDirection == "asc" ?
                            query.OrderBy(q => q.RegiaoAutorizada.PA.ToString()) :
                            query.OrderByDescending(q => q.RegiaoAutorizada.PA.ToString());
                        break;
                    case "nomeLocal":
                        query = sortDirection == "asc" ?
                            query.OrderBy(q => q.LocalAtendimento.NomeLocal) :
                            query.OrderByDescending(q => q.LocalAtendimento.NomeLocal);
                        break;

                    case "numBanco":
                        query = sortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Cliente.NumBanco))
                                 .OrderBy(q => q.Cliente.NumBanco) :
                            query.Where(q => !string.IsNullOrEmpty(q.Cliente.NumBanco))
                                 .OrderByDescending(q => q.Cliente.NumBanco);
                        break;

                    case "nomeEquip":
                        query = sortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Equipamento.NomeEquip))
                                 .OrderBy(q => q.Equipamento.NomeEquip) :
                        query.Where(q => !string.IsNullOrEmpty(q.Equipamento.NomeEquip))
                             .OrderByDescending(q => q.Equipamento.NomeEquip);
                        break;

                    case "nomeSLA":
                        query = sortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.AcordoNivelServico.NomeSLA))
                                 .OrderBy(q => q.EquipamentoContrato.AcordoNivelServico.NomeSLA) :
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.AcordoNivelServico.NomeSLA))
                                .OrderByDescending(q => q.EquipamentoContrato.AcordoNivelServico.NomeSLA);
                        break;

                    case "numSerie":
                        query = sortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.NumSerie))
                                 .OrderBy(q => q.EquipamentoContrato.NumSerie) :
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.NumSerie))
                                 .OrderByDescending(q => q.EquipamentoContrato.NumSerie);
                        break;

                    case "nomeTecnico":
                        query = sortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Tecnico.Nome) && q.CodStatusServico == 8)
                                 .OrderBy(q => q.Tecnico.Nome) :
                            query.Where(q => !string.IsNullOrEmpty(q.Tecnico.Nome) && q.CodStatusServico == 8)
                                 .OrderByDescending(q => q.Tecnico.Nome);
                        break;

                    case "datahoraAberturaOS":
                        query = sortDirection == "asc" ?
                            query.Where(s => s.DataHoraAberturaOS != null).OrderBy(q => q.DataHoraAberturaOS) :
                            query.Where(s => s.DataHoraAberturaOS != null).OrderByDescending(q => q.DataHoraAberturaOS);
                        break;

                    case "statusSLA":
                        break;

                    default:
                        query = query.OrderBy(string.Format("{0} {1}", sortActive, sortDirection));
                        break;
                }
            }

            return query;
        }
    }
}
