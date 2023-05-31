using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoBanrisulATM")]
    public partial class IntegracaoBanrisulAtm
    {
        [Key]
        [Column("CodIntegracaoBanrisulATM")]
        public int CodIntegracaoBanrisulAtm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraEnvioFechamentos { get; set; }
    }
}
