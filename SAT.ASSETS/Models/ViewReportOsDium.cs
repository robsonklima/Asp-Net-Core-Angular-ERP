using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ViewReportOsDium
    {
        [Column("nomecliente")]
        [StringLength(50)]
        public string Nomecliente { get; set; }
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Column("codos")]
        public int Codos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [StringLength(255)]
        public string NomeSolicitante { get; set; }
        [StringLength(20)]
        public string TelefoneSolicitante { get; set; }
        [StringLength(50)]
        public string NomeAcompanhante { get; set; }
        [StringLength(50)]
        public string NomeRespCliente { get; set; }
        public int? NumReincidencia { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("NomeUF")]
        [StringLength(50)]
        public string NomeUf { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
    }
}
