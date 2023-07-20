using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class OrdemServicoParameters : QueryStringParameters
    {
        public string CodOS { get; set; }
        public string CodOSs { get; set; }
        public int? CodEquipContrato { get; set; }
        public string CodTecnicos { get; set; }
        public string NumOSCliente { get; set; }
        public string NumOSQuarteirizada { get; set; }
        public string PAS { get; set; }
        public string CodStatusServicos { get; set; }
        public string CodTiposIntervencao { get; set; }
        public string CodClientes { get; set; }
        public int? CodCliente { get; set; }
        public string CodFiliais { get; set; }
        public string CodAutorizadas { get; set; }
        public string CodEquipamentos { get; set; }
        public string CodTiposGrupo { get; set; }
        public string CodRegioes { get; set; }
        public string CodPostos { get; set; }
        public string NumSerie { get; set; }
        public byte? IndIntegracao { get; set; }
        public DateTime? DataAberturaInicio { get; set; }
        public DateTime? DataAberturaFim { get; set; }
        public DateTime? DataFechamentoInicio { get; set; }
        public DateTime? DataFechamentoFim { get; set; }
        public DateTime? DataHoraManutInicio { get; set; }
        public DateTime? DataHoraManutFim { get; set; }
        public DateTime? DataTransfInicio { get; set; }
        public DateTime? DataTransfFim { get; set; }
        public DateTime? DataCancelamentoInicio { get; set; }
        public DateTime? DataCancelamentoFim { get; set; }
        public OrdemServicoIncludeEnum Include { get; set; }
        public OrdemServicoFilterEnum FilterType { get; set; }
        public string PontosEstrategicos { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndServico { get; set; }
        public int? CodContrato { get; set; }
        public int? PA { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public string NotIn_CodStatusServicos { get; set; }
        public DateTime? DataInicioDispBB { get; set; }
        public DateTime? DataFimDispBB { get; set; }
        public DateTime? DataHoraInicioInicio { get; set; }
        public DateTime? DataHoraInicioFim { get; set; }
        public DateTime? InicioPeriodoAgenda { get; set; }
        public DateTime? FimPeriodoAgenda { get; set; }
        public DateTime? DataHoraSolucaoInicio { get; set; }
        public DateTime? DataHoraSolucaoFim { get; set; }
        public string Defeito { get; set; }
        public string Solucao { get; set; }
        public string CodTipoIntervencaoNotIn { get; set; }
        public string CodUsuariosSTN { get; set; }
        public string NumRAT { get; set; }
        public bool DataHoraManutNull { get; set; }
        public bool CodEquipIsNull { get; set; }
    }
}