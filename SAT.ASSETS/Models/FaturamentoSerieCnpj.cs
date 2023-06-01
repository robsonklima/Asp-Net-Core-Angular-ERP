using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FaturamentoSerieCNPJ")]
    public partial class FaturamentoSerieCnpj
    {
        public int AnoMes { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodEquipContrato { get; set; }
        [Required]
        [Column("CNPJFaturamento")]
        [StringLength(30)]
        public string Cnpjfaturamento { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
