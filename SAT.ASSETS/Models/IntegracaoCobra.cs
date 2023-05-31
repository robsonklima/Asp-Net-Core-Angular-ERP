using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("IntegracaoCobra")]
    public partial class IntegracaoCobra
    {
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(50)]
        public string NumOscliente { get; set; }
        [StringLength(50)]
        public string NomeTipoArquivoEnviado { get; set; }
        [StringLength(500)]
        public string NomeArquivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvio { get; set; }
    }
}
