using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSREC")]
    public partial class Posrec
    {
        [StringLength(255)]
        public string Rede { get; set; }
        [Column("CNPJ / CPF")]
        public double? CnpjCpf { get; set; }
        public double? Terminal { get; set; }
        [Column("Nome Fantasia")]
        [StringLength(255)]
        public string NomeFantasia { get; set; }
        [Column("Tip Solicitação")]
        [StringLength(255)]
        public string TipSolicitação { get; set; }
        [Column("Data Solicitação", TypeName = "datetime")]
        public DateTime? DataSolicitação { get; set; }
        [StringLength(255)]
        public string Situação { get; set; }
    }
}
