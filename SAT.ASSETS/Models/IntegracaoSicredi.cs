using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoSicredi")]
    public partial class IntegracaoSicredi
    {
        [Key]
        public int CodIntegracaoSicredi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraEnvioFechamentos { get; set; }
    }
}
