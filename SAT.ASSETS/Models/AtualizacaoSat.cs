using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AtualizacaoSAT")]
    public partial class AtualizacaoSat
    {
        public AtualizacaoSat()
        {
            AtualizacaoUsuarioSats = new HashSet<AtualizacaoUsuarioSat>();
        }

        [Key]
        [Column("CodAtualizacaoSAT")]
        public int CodAtualizacaoSat { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [StringLength(800)]
        public string Descricao { get; set; }
        [StringLength(100)]
        public string NomeArquivo { get; set; }
        public int QtdDiasValidade { get; set; }
        public int? Importancia { get; set; }
        public byte? IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }

        [InverseProperty(nameof(AtualizacaoUsuarioSat.CodAtualizacaoSatNavigation))]
        public virtual ICollection<AtualizacaoUsuarioSat> AtualizacaoUsuarioSats { get; set; }
    }
}
