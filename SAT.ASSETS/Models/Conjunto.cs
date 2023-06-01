using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Conjunto")]
    public partial class Conjunto
    {
        public Conjunto()
        {
            ConjuntoConjuntoCodConjuntoFilhoNavigations = new HashSet<ConjuntoConjunto>();
            ConjuntoConjuntoCodConjuntoNavigations = new HashSet<ConjuntoConjunto>();
            ConjuntoPecas = new HashSet<ConjuntoPeca>();
        }

        [Key]
        public int CodConjunto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeConjunto { get; set; }
        [StringLength(200)]
        public string DescConjunto { get; set; }
        public int? CodPecaBase { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodPecaBase))]
        [InverseProperty(nameof(Peca.Conjuntos))]
        public virtual Peca CodPecaBaseNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.ConjuntoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.ConjuntoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(ConjuntoConjunto.CodConjuntoFilhoNavigation))]
        public virtual ICollection<ConjuntoConjunto> ConjuntoConjuntoCodConjuntoFilhoNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoConjunto.CodConjuntoNavigation))]
        public virtual ICollection<ConjuntoConjunto> ConjuntoConjuntoCodConjuntoNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoPeca.CodConjuntoNavigation))]
        public virtual ICollection<ConjuntoPeca> ConjuntoPecas { get; set; }
    }
}
