using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSLogix")]
    public partial class Poslogix
    {
        [Column("OPERACAO")]
        public string Operacao { get; set; }
        [Column("PAGAMENTO")]
        public string Pagamento { get; set; }
        [Column("CLIENTE")]
        public string Cliente { get; set; }
        [Column("NOME")]
        public string Nome { get; set; }
        [Column("ESTABELECIMENTO")]
        public string Estabelecimento { get; set; }
        [Column("VALORRECEBER")]
        public string Valorreceber { get; set; }
        [Column("VALORARQUIVO")]
        public string Valorarquivo { get; set; }
        [Column("OBSERVAÇAO")]
        public string Observaçao { get; set; }
    }
}
