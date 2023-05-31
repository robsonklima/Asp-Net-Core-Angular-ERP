using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class DboVwcCnpjClientesLocai
    {
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Column("CNPJ Cliente")]
        [StringLength(20)]
        public string CnpjCliente { get; set; }
        [StringLength(50)]
        public string NroContrato { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("CNPJFaturamento")]
        [StringLength(20)]
        public string Cnpjfaturamento { get; set; }
        [Column("CNPJ Local")]
        [StringLength(20)]
        public string CnpjLocal { get; set; }
        public int CodPosto { get; set; }
        public int CodCliente { get; set; }
    }
}
