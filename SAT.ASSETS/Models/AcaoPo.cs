using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AcaoPOS")]
    public partial class AcaoPo
    {
        public AcaoPo()
        {
            AtendimentoTelefonicoPosacaos = new HashSet<AtendimentoTelefonicoPosacao>();
        }

        [Key]
        [Column("CodAcaoPOS")]
        public int CodAcaoPos { get; set; }
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        [StringLength(20)]
        public string Cor { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }
        public bool Ativo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataManutencao { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.AcaoPoCodUsuarioCadastroNavigations))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManutencao))]
        [InverseProperty(nameof(Usuario.AcaoPoCodUsuarioManutencaoNavigations))]
        public virtual Usuario CodUsuarioManutencaoNavigation { get; set; }
        [InverseProperty(nameof(AtendimentoTelefonicoPosacao.CodAcaoPosNavigation))]
        public virtual ICollection<AtendimentoTelefonicoPosacao> AtendimentoTelefonicoPosacaos { get; set; }
    }
}
