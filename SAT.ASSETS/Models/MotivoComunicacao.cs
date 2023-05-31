using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoComunicacao")]
    public partial class MotivoComunicacao
    {
        public MotivoComunicacao()
        {
            Ratbanrisuls = new HashSet<Ratbanrisul>();
        }

        [Key]
        public int CodMotivoComunicacao { get; set; }
        [Required]
        [Column("MotivoComunicacao")]
        [StringLength(200)]
        public string MotivoComunicacao1 { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [InverseProperty(nameof(Ratbanrisul.CodMotivoComunicacaoNavigation))]
        public virtual ICollection<Ratbanrisul> Ratbanrisuls { get; set; }
    }
}
