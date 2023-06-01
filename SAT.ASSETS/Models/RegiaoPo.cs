using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RegiaoPOS")]
    public partial class RegiaoPo
    {
        public RegiaoPo()
        {
            Cidades = new HashSet<Cidade>();
        }

        [Key]
        [Column("CodRegiaoPOS")]
        public int CodRegiaoPos { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        public bool Ativo { get; set; }

        [InverseProperty(nameof(Cidade.CodRegiaoPosNavigation))]
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
