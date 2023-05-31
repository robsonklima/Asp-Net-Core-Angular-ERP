using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DefeitoPOS")]
    public partial class DefeitoPo
    {
        public DefeitoPo()
        {
            Chamados = new HashSet<Chamado>();
            EquipamentoCheckListDefeitos = new HashSet<EquipamentoCheckListDefeito>();
            EquipamentoDefeitoPos = new HashSet<EquipamentoDefeitoPo>();
            FecharOspos = new HashSet<FecharOspo>();
            Os = new HashSet<O>();
            OsArqMortos = new HashSet<OsArqMorto>();
        }

        [Key]
        [Column("CodDefeitoPOS")]
        public int CodDefeitoPos { get; set; }
        [Required]
        [Column("DefeitoPOS")]
        [StringLength(200)]
        public string DefeitoPos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        public int? CodDefeito { get; set; }
        public bool? ExigeTrocaEquipamento { get; set; }

        [ForeignKey(nameof(CodDefeito))]
        [InverseProperty(nameof(Defeito.DefeitoPos))]
        public virtual Defeito CodDefeitoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.DefeitoPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(Chamado.CodDefeitoPosNavigation))]
        public virtual ICollection<Chamado> Chamados { get; set; }
        [InverseProperty(nameof(EquipamentoCheckListDefeito.CodDefeitoPosNavigation))]
        public virtual ICollection<EquipamentoCheckListDefeito> EquipamentoCheckListDefeitos { get; set; }
        [InverseProperty(nameof(EquipamentoDefeitoPo.CodDefeitoPosNavigation))]
        public virtual ICollection<EquipamentoDefeitoPo> EquipamentoDefeitoPos { get; set; }
        [InverseProperty(nameof(FecharOspo.CodDefeitoPosNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(O.CodDefeitoPosNavigation))]
        public virtual ICollection<O> Os { get; set; }
        [InverseProperty(nameof(OsArqMorto.CodDefeitoPosNavigation))]
        public virtual ICollection<OsArqMorto> OsArqMortos { get; set; }
    }
}
