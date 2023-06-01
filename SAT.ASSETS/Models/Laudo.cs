using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Laudo")]
    public partial class Laudo
    {
        [Key]
        public int CodLaudo { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        public int CodTecnico { get; set; }
        [Required]
        [StringLength(2000)]
        public string RelatoCliente { get; set; }
        [Required]
        [StringLength(2000)]
        public string Conclusao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public int CodLaudoStatus { get; set; }
        [StringLength(90)]
        public string NomeCliente { get; set; }
        [StringLength(50)]
        public string MatriculaCliente { get; set; }
        [StringLength(50)]
        public string TensaoComCarga { get; set; }
        [StringLength(50)]
        public string TensaoSemCarga { get; set; }
        [Column("TensaoTerraENeutro")]
        [StringLength(50)]
        public string TensaoTerraEneutro { get; set; }
        [StringLength(50)]
        public string Temperatura { get; set; }
        [StringLength(160)]
        public string Justificativa { get; set; }
        public int? IndRedeEstabilizada { get; set; }
        public int? IndPossuiNobreak { get; set; }
        public int? IndPossuiArCond { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? IndAtivo { get; set; }
    }
}
