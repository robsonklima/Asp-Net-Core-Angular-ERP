using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UF")]
    public partial class Uf
    {
        public Uf()
        {
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            CalendarioDiaFeriados = new HashSet<CalendarioDiaFeriado>();
            Cidades = new HashSet<Cidade>();
            EstadoPosdeParas = new HashSet<EstadoPosdePara>();
            FeriadosPos = new HashSet<FeriadosPo>();
            LocalEnvioNffaturamentoCodUfenvioNfNavigations = new HashSet<LocalEnvioNffaturamento>();
            LocalEnvioNffaturamentoCodUffaturamentoNavigations = new HashSet<LocalEnvioNffaturamento>();
        }

        [Key]
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int CodPais { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Required]
        [Column("NomeUF")]
        [StringLength(50)]
        public string NomeUf { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.UfCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.UfCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodUfNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(CalendarioDiaFeriado.CodUfNavigation))]
        public virtual ICollection<CalendarioDiaFeriado> CalendarioDiaFeriados { get; set; }
        [InverseProperty(nameof(Cidade.CodUfNavigation))]
        public virtual ICollection<Cidade> Cidades { get; set; }
        [InverseProperty(nameof(EstadoPosdePara.CodUfNavigation))]
        public virtual ICollection<EstadoPosdePara> EstadoPosdeParas { get; set; }
        [InverseProperty(nameof(FeriadosPo.CodUfNavigation))]
        public virtual ICollection<FeriadosPo> FeriadosPos { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodUfenvioNfNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentoCodUfenvioNfNavigations { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodUffaturamentoNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentoCodUffaturamentoNavigations { get; set; }
    }
}
