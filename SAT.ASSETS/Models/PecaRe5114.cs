using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecaRE5114")]
    [Index(nameof(NumRe5114), Name = "IX_PecaRE5114", IsUnique = true)]
    public partial class PecaRe5114
    {
        public PecaRe5114()
        {
            OsbancadaPecas = new HashSet<OsbancadaPeca>();
        }

        [Key]
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        [Column("NumRE5114")]
        [StringLength(20)]
        public string NumRe5114 { get; set; }
        public int CodPeca { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(20)]
        public string NumPecaCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        public byte? IndSucata { get; set; }
        public byte? IndDevolver { get; set; }
        public byte? IndMarcacaoEspecial { get; set; }
        [StringLength(1000)]
        public string MotivoDevolucao { get; set; }
        [StringLength(1000)]
        public string MotivoSucata { get; set; }
        [Column("CodOSBancada")]
        public int? CodOsbancada { get; set; }

        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PecaRe5114s))]
        public virtual Peca CodPecaNavigation { get; set; }
        [InverseProperty(nameof(OsbancadaPeca.CodPecaRe5114Navigation))]
        public virtual ICollection<OsbancadaPeca> OsbancadaPecas { get; set; }
    }
}
