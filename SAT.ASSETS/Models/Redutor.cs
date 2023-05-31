using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("REDUTOR")]
    public partial class Redutor
    {
        [Column("ANO")]
        [StringLength(255)]
        public string Ano { get; set; }
        [Column("MÊS")]
        [StringLength(255)]
        public string Mês { get; set; }
        [Column("NUMEROOS")]
        [StringLength(255)]
        public string Numeroos { get; set; }
        [Column("NUMEROBEM")]
        [StringLength(255)]
        public string Numerobem { get; set; }
        [Column("REGIONAL")]
        [StringLength(255)]
        public string Regional { get; set; }
        [Column("CAT")]
        [StringLength(255)]
        public string Cat { get; set; }
        [Column("MANTENEDOR")]
        [StringLength(255)]
        public string Mantenedor { get; set; }
        [Column("CONTRATO")]
        [StringLength(255)]
        public string Contrato { get; set; }
        [Column("DENTRODOPRAZO")]
        [StringLength(255)]
        public string Dentrodoprazo { get; set; }
        [Column("CRITICIDADE")]
        [StringLength(255)]
        public string Criticidade { get; set; }
        [Column("DTACHAMADA", TypeName = "datetime")]
        public DateTime? Dtachamada { get; set; }
        [Column("DTAFIM", TypeName = "datetime")]
        public DateTime? Dtafim { get; set; }
        [Column("DTALIMITE", TypeName = "datetime")]
        public DateTime? Dtalimite { get; set; }
        [Column("DTAAGENDAMENTO", TypeName = "datetime")]
        public DateTime? Dtaagendamento { get; set; }
        [Column("MOTIVO")]
        [StringLength(255)]
        public string Motivo { get; set; }
        [Column("GAPHORA")]
        [StringLength(255)]
        public string Gaphora { get; set; }
        [Column("GAP")]
        public double? Gap { get; set; }
        [Column("Até 3 h")]
        public double? Até3H { get; set; }
        [Column("Entre 3 e 6 h")]
        public double? Entre3E6H { get; set; }
        [Column("Entre 6 e 12 h")]
        public double? Entre6E12H { get; set; }
        [Column("Entre 12 e 20 h")]
        public double? Entre12E20H { get; set; }
        [Column("Entre 20 e 30 h")]
        public double? Entre20E30H { get; set; }
        [Column("Entre 30 e 50 h")]
        public double? Entre30E50H { get; set; }
        [Column("Mais de 50 h")]
        public double? MaisDe50H { get; set; }
        [Column("VALOR")]
        public double? Valor { get; set; }
        [Column("GAP_EDITADO")]
        [StringLength(255)]
        public string GapEditado { get; set; }
        [Column("SEMAT")]
        [StringLength(255)]
        public string Semat { get; set; }
        [Column("DISTANCIA")]
        [StringLength(255)]
        public string Distancia { get; set; }
        [Column("HORASDISTANCIA")]
        [StringLength(255)]
        public string Horasdistancia { get; set; }
        [Column("TACESSO")]
        [StringLength(255)]
        public string Tacesso { get; set; }
        [Column("HADICIONAR")]
        public double? Hadicionar { get; set; }
        [Column("RHORARIO")]
        public double? Rhorario { get; set; }
        [Column("DTAGERACAO", TypeName = "datetime")]
        public DateTime? Dtageracao { get; set; }
    }
}
