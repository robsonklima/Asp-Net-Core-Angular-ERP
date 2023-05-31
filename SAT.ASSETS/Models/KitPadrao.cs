using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("KitPadrao")]
    public partial class KitPadrao
    {
        public int CodKitPadrao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeKitPadrao { get; set; }
        public int CodCliente { get; set; }
        public int IndAtivo { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
