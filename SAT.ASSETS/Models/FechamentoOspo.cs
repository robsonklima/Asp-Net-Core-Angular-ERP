using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FechamentoOSPOS")]
    public partial class FechamentoOspo
    {
        [Key]
        [Column("CodFechamentoOSPOS")]
        public int CodFechamentoOspos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int? Corretiva { get; set; }
        public int? Reconfiguracao { get; set; }
        public int? Instalacao { get; set; }
        public int? TrocaViaArquivo { get; set; }
        public int? TrocaViaIncidente { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.FechamentoOspos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
