using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PecaFamilium
    {
        public PecaFamilium()
        {
            Pecas = new HashSet<Peca>();
        }

        [Key]
        public int CodPecaFamilia { get; set; }
        public int CodPecaBase { get; set; }
        [Required]
        [StringLength(80)]
        public string NomeFamilia { get; set; }
        [StringLength(100)]
        public string DescFamilia { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodPecaBase))]
        [InverseProperty(nameof(Peca.PecaFamilia))]
        public virtual Peca CodPecaBaseNavigation { get; set; }
        [InverseProperty(nameof(Peca.CodPecaFamiliaNavigation))]
        public virtual ICollection<Peca> Pecas { get; set; }
    }
}
