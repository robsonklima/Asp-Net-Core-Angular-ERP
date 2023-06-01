using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwChamadosUploadRatDigitalizadum
    {
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolucao { get; set; }
        [Column("OS")]
        public int Os { get; set; }
        [Column("Data Ab. OS", TypeName = "datetime")]
        public DateTime DataAbOs { get; set; }
        [Column("Data Solic.", TypeName = "datetime")]
        public DateTime DataSolic { get; set; }
        [Required]
        [Column("OS Cliente")]
        [StringLength(20)]
        public string OsCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Intervenção { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [Required]
        [Column("PAT")]
        [StringLength(50)]
        public string Pat { get; set; }
        [Required]
        [StringLength(50)]
        public string Região { get; set; }
        [Required]
        [StringLength(50)]
        public string Cliente { get; set; }
        [Required]
        [StringLength(5)]
        public string Agencia { get; set; }
        [Required]
        [Column("DC")]
        [StringLength(4)]
        public string Dc { get; set; }
        [Required]
        [StringLength(50)]
        public string Local { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Required]
        [StringLength(50)]
        public string Equipamento { get; set; }
        [Required]
        [StringLength(100)]
        public string Contrato { get; set; }
        [Required]
        [Column("Tipo Contrato")]
        [StringLength(50)]
        public string TipoContrato { get; set; }
        [Required]
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Required]
        [Column("N. Serie")]
        [StringLength(20)]
        public string NSerie { get; set; }
        [Required]
        [Column("Num RAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [Required]
        [Column("Status RAT")]
        [StringLength(50)]
        public string StatusRat { get; set; }
        [Required]
        [StringLength(50)]
        public string Técnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Início { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Termino { get; set; }
    }
}
