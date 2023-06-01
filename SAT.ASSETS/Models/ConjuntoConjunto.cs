using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ConjuntoConjunto")]
    public partial class ConjuntoConjunto
    {
        [Key]
        public int CodConjuntoConjunto { get; set; }
        public int CodConjunto { get; set; }
        public int CodConjuntoFilho { get; set; }
        public int? CodPecaAssociada { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodConjuntoFilho))]
        [InverseProperty(nameof(Conjunto.ConjuntoConjuntoCodConjuntoFilhoNavigations))]
        public virtual Conjunto CodConjuntoFilhoNavigation { get; set; }
        [ForeignKey(nameof(CodConjunto))]
        [InverseProperty(nameof(Conjunto.ConjuntoConjuntoCodConjuntoNavigations))]
        public virtual Conjunto CodConjuntoNavigation { get; set; }
        [ForeignKey(nameof(CodPecaAssociada))]
        [InverseProperty(nameof(Peca.ConjuntoConjuntos))]
        public virtual Peca CodPecaAssociadaNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.ConjuntoConjuntoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.ConjuntoConjuntoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
    }
}
