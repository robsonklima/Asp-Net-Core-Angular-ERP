using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ConjuntoPeca")]
    public partial class ConjuntoPeca
    {
        [Key]
        public int CodConjunto { get; set; }
        [Key]
        public int CodPeca { get; set; }
        public int Qtd1 { get; set; }
        public int Qtd2 { get; set; }
        public int Qtd3 { get; set; }
        public int Qtd4 { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodConjunto))]
        [InverseProperty(nameof(Conjunto.ConjuntoPecas))]
        public virtual Conjunto CodConjuntoNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.ConjuntoPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.ConjuntoPecaCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.ConjuntoPecaCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
    }
}
