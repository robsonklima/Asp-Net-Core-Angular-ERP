using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SLA")]
    public partial class Sla
    {
        public Sla()
        {
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            ContratoSlas = new HashSet<ContratoSla>();
            Contratos = new HashSet<Contrato>();
            ValorServicos = new HashSet<ValorServico>();
        }

        [Key]
        [Column("CodSLA")]
        public int CodSla { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [Column("DescSLA")]
        [StringLength(300)]
        public string DescSla { get; set; }
        public float? TempoInicio { get; set; }
        public float? TempoReparo { get; set; }
        public float? TempoSolucao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioFim { get; set; }
        [Column("KMHora")]
        public int? Kmhora { get; set; }
        [Column("KMHoraExtra")]
        public int? KmhoraExtra { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        public byte? IndAgendamento { get; set; }
        public byte? IndSabado { get; set; }
        public byte? IndDomingo { get; set; }
        public byte? IndFeriado { get; set; }

        [InverseProperty(nameof(AutorizadaRepasse.CodSlaNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(ContratoSla.CodSlaNavigation))]
        public virtual ICollection<ContratoSla> ContratoSlas { get; set; }
        [InverseProperty(nameof(Contrato.CodSlaNavigation))]
        public virtual ICollection<Contrato> Contratos { get; set; }
        [InverseProperty(nameof(ValorServico.CodSlaNavigation))]
        public virtual ICollection<ValorServico> ValorServicos { get; set; }
    }
}
