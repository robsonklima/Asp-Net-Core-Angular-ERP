using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcSqlMaribelStn
    {
        public int Protocolo { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DT. Abert. Protocolo STN")]
        [StringLength(4000)]
        public string DtAbertProtocoloStn { get; set; }
        [Column("Hora Abertura OS STN")]
        [StringLength(4000)]
        public string HoraAberturaOsStn { get; set; }
        [Column("Fechamento OS STN")]
        [StringLength(4000)]
        public string FechamentoOsStn { get; set; }
        [StringLength(4000)]
        public string Período { get; set; }
        [Column("numReincidenciaAoAssumir")]
        public int? NumReincidenciaAoAssumir { get; set; }
        [Column("DescStatusServicoSTN")]
        [StringLength(50)]
        public string DescStatusServicoStn { get; set; }
        [Column("DescOrigemChamadoSTN")]
        [StringLength(100)]
        public string DescOrigemChamadoStn { get; set; }
        [Column("numTratativas")]
        public int? NumTratativas { get; set; }
        [Column("DataHoraAberturaSTN", TypeName = "datetime")]
        public DateTime DataHoraAberturaStn { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("Responsável OS STN")]
        [StringLength(50)]
        public string ResponsávelOsStn { get; set; }
        [Column("qtdOS90")]
        public int? QtdOs90 { get; set; }
        [Column("MTBF90")]
        public double Mtbf90 { get; set; }
        [Column("qtdOS posTratativa")]
        public int? QtdOsPosTratativa { get; set; }
        [Column("MTBF POS Tratativa")]
        public int MtbfPosTratativa { get; set; }
        [Column("Nome Solicitante")]
        [StringLength(200)]
        public string NomeSolicitante { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [Required]
        [Column("Suporte Tratativa")]
        [StringLength(50)]
        public string SuporteTratativa { get; set; }
        [Column("Data Cadastro Tratativa")]
        [StringLength(4000)]
        public string DataCadastroTratativa { get; set; }
        [Required]
        [Column("Demanda em espera")]
        [StringLength(3)]
        public string DemandaEmEspera { get; set; }
        [Required]
        [Column("Primeiro Contato")]
        [StringLength(50)]
        public string PrimeiroContato { get; set; }
        [Column("Código Tratativa")]
        public int? CódigoTratativa { get; set; }
        [Required]
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(73)]
        public string Produto { get; set; }
        [Required]
        [Column("Local do Atendimento")]
        [StringLength(50)]
        public string LocalDoAtendimento { get; set; }
        [Required]
        [Column("Relato Técnico")]
        [StringLength(2000)]
        public string RelatoTécnico { get; set; }
        [Required]
        [Column("Orientação do Suporte")]
        public string OrientaçãoDoSuporte { get; set; }
        [Required]
        [Column("Tipo de Chamado Tratativa")]
        [StringLength(100)]
        public string TipoDeChamadoTratativa { get; set; }
        [Required]
        [StringLength(3)]
        public string Treinamento { get; set; }
        [Required]
        [StringLength(3)]
        public string Diagnostico { get; set; }
        [Required]
        [StringLength(3)]
        public string Manual { get; set; }
        [Required]
        [Column("Netbook/Ferramenta")]
        [StringLength(3)]
        public string NetbookFerramenta { get; set; }
        [Required]
        [Column("Evitou Pendência")]
        [StringLength(3)]
        public string EvitouPendência { get; set; }
        [StringLength(500)]
        public string ObsSistema { get; set; }
    }
}
