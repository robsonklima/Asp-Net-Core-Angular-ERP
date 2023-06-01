using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AtendimentoTelefonicoPOS")]
    public partial class AtendimentoTelefonicoPo
    {
        public AtendimentoTelefonicoPo()
        {
            AtendimentoTelefonicoPosacaos = new HashSet<AtendimentoTelefonicoPosacao>();
        }

        [Key]
        [Column("CodAtendimentoTelefonicoPOS")]
        public int CodAtendimentoTelefonicoPos { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(5000)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraAtendimento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Required]
        [StringLength(200)]
        public string Tecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        public bool Ativo { get; set; }
        [StringLength(200)]
        public string Contato { get; set; }
        public bool EmailEnviado { get; set; }
        [Required]
        [Column("BaseURL")]
        [StringLength(300)]
        public string BaseUrl { get; set; }
        [Column("CancelarOS")]
        public bool? CancelarOs { get; set; }
        [Column("CancelarOSAtendimento")]
        public bool? CancelarOsatendimento { get; set; }
        [Column("FecharOS")]
        public bool? FecharOs { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.AtendimentoTelefonicoPos))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.AtendimentoTelefonicoPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(AtendimentoTelefonicoPosacao.CodAtendimentoTelefonicoPosNavigation))]
        public virtual ICollection<AtendimentoTelefonicoPosacao> AtendimentoTelefonicoPosacaos { get; set; }
    }
}
