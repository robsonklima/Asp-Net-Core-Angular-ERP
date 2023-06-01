using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class EquipamentoContrato
    {
        public int CodEquipContrato { get; set; }
        public int? CodContrato { get; set; }
        public virtual Contrato Contrato { get; set; }
        public int CodTipoEquip { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
        public int CodGrupoEquip { get; set; }
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public int? CodEquip { get; set; }
        public Equipamento Equipamento { get; set; }
        public int CodSLA { get; set; }
        public AcordoNivelServico AcordoNivelServico { get; set; }
        public string NumSerie { get; set; }
        public string NumSerieCliente { get; set; }
        public int CodCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int CodPosto { get; set; }
        public virtual LocalAtendimento LocalAtendimento { get; set; }
        public int CodRegiao { get; set; }
        public Regiao Regiao { get; set; }
        public int CodAutorizada { get; set; }
        public Autorizada Autorizada { get; set; }
        public int CodFilial { get; set; }
        public Filial Filial { get; set; }
        [Column("DistanciaPAT_Res")]
        public decimal? DistanciaPatRes { get; set; }
        public byte? IndGarantia { get; set; }
        public DateTime? DataInicGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        public byte? IndReceita { get; set; }
        public decimal? ValorReceita { get; set; }
        public byte? IndRepasse { get; set; }
        public byte? IndRepasseIndividual { get; set; }
        public decimal? ValorRepasse { get; set; }
        public decimal? ValorDespesa { get; set; }
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte? IndInstalacao { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public DateTime? DataDesativacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string Origem { get; set; }
        public DateTime? HoraDispInicio { get; set; }
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        public string CodEquipCliente { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public byte? IndSemat { get; set; }
        public string PontoEstrategico { get; set; }
        public byte? IndRHorario { get; set; }
        public byte? IndRAcesso { get; set; }
        public int? CodTipoLocalAtendimento { get; set; }
        public byte? IndVerao { get; set; }
        public byte? IndPAE { get; set; }
        public byte? IndRetrofit { get; set; }
        public DateTime? DataRetrofit1 { get; set; }
        public DateTime? DataRetrofit2 { get; set; }
        public byte? IndRetrofit2 { get; set; }
        public byte? IndRetrofit3 { get; set; }
        public DateTime? DataRetrofit3 { get; set; }
        public byte? IndMTBF4 { get; set; }
        public string NumEtiquetaEquip { get; set; }
        public DateTime? DataRelatorio { get; set; }
        public byte? IndRelatorio { get; set; }
        public string CodBMP { get; set; }
        public string Sequencia { get; set; }
        public byte? IndMecanismo { get; set; }
        public int? CodDispBBCriticidade { get; set; }
        public DispBBCriticidade DispBBCriticidade { get; set; }
        public string AtmId { get; set; }
        [ForeignKey("CodFilial, CodRegiao, CodAutorizada")]
        public RegiaoAutorizada RegiaoAutorizada { get; set; }
        [ForeignKey("CodContrato, CodEquip")]
        public ContratoEquipamento ContratoEquipamento { get; set; }
        public MtbfEquipamento Mtbf { get; set; }
        public decimal? ValorSoftwareEmbarcado { get; set; }
        public decimal? ValorMonitoramentoRemoto { get; set; }
    }
}
