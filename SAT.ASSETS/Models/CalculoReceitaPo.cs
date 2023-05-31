using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CalculoReceitaPOS")]
    public partial class CalculoReceitaPo
    {
        [Column("CNPJ")]
        [StringLength(255)]
        public string Cnpj { get; set; }
        [StringLength(255)]
        public string Terminal { get; set; }
        [StringLength(255)]
        public string Rede { get; set; }
        [StringLength(255)]
        public string Estabelecimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInst { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusaoBanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        [StringLength(255)]
        public string Tabela { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorManutencao { get; set; }
    }
}
