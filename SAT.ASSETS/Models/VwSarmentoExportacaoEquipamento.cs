using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwSarmentoExportacaoEquipamento
    {
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("Agência/DCPosto")]
        [StringLength(10)]
        public string AgênciaDcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
    }
}
