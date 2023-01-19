using System;

namespace SAT.MODELS.Views
{
    public class ViewExportacaoChamadosUnificado
    {
        public int? CodOS { get; set; }
        public string NumOSCliente { get; set; }
        public string NumOSQuarteirizada { get; set; }
        public DateTime? DataHoraIntegracao { get; set; }
        public DateTime? DataHoraAberturaOS { get; set; }
        public DateTime? DataHoraFechamento { get; set; }
        public DateTime? DataHoraTransf { get; set; }
        public DateTime? DataHoraSolicitacao { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public DateTime? DataHoraSolucao { get; set; }
        public string StatusSLAOS { get; set; }
        public string StatusOS { get; set; }
        public int? CodStatusServico { get; set; }
        public string Intervencao { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodFilial { get; set; }
        public string Filial { get; set; }
        public string Autorizada { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public string Regiao { get; set; }
        public string Cliente { get; set; }
        public int? CodCliente { get; set; }
        public string NumBanco { get; set; }
        public int? CodPosto{ get; set; }
        public string LocalAtendimento { get; set; }
        public int? PA { get; set; }
        public string Agencia { get; set; }
        public string DCPosto { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string SiglaUF { get; set; }
        public string Pais { get; set; }
        public int? CodEquip { get; set; }
        public string Equipamento { get; set; }
        public int? CodGrupoEquip{ get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodContrato { get; set; }
        public string NroContrato { get; set; }
        public string NumSerie { get; set; }
        public int? NumReincidencia { get; set; }
        public string NumSerieCliente { get; set; }
        public DateTime? DataFimGarantia { get; set; }
        public string PontoEstrategico { get; set; }
        public string SLA { get; set; }
        public string DefeitoRelatado { get; set; }
        public string ObservacaoCliente { get; set; }
        public DateTime? DataHoraCancelamento { get; set; }
        public string MotivoCancelamento { get; set; }
        public int? CodRAT { get; set; }
        public string NumRAT { get; set; }
        public string StatusRAT { get; set; }
        public string Tecnico { get; set; }
        public int? CodTecnico { get; set; }
        public string RGTecnico { get; set; }
        public string TipoServico { get; set; }
        public DateTime? DataHoraChegada { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public int? TempoAtendimentoMin { get; set; }
        public string RelatoSolucao { get; set; }
        public string ObsRAT { get; set; }
        public int? CodRATDetalhe { get; set; }
        public string CodEDefeito { get; set; }
        public string NomeDefeito { get; set; }
        public string CodECausa { get; set; }
        public string NomeCausa { get; set; }
        public string CodEAcao { get; set; }
        public string NomeAcao { get; set; }
        public int? CodRATDetalhesPecas { get; set; }
        public string CodMagnus { get; set; }
        public string NomePeca { get; set; }
        public int? QtdePecas { get; set; }
        public byte? IndServico { get; set; }
    }
}
