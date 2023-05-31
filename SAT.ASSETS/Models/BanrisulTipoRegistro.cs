using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("BanrisulTipoRegistro")]
    public partial class BanrisulTipoRegistro
    {
        public BanrisulTipoRegistro()
        {
            BanrisulInterfaces = new HashSet<BanrisulInterface>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [InverseProperty(nameof(BanrisulInterface.IdtipoRegistroNavigation))]
        public virtual ICollection<BanrisulInterface> BanrisulInterfaces { get; set; }
    }
}
