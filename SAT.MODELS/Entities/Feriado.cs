using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("Feriados")]
    public class Feriado
    {
        [Key]
        public int? CodFeriado { get; set; }
        public string NomeFeriado { get; set; }
        public DateTime? Data { get; set; }
        public short? QtdeDias { get; set; }
        public int? CodPais { get; set; }
        [ForeignKey("CodPais")]
        public Pais Pais { get; set; }
        public int? CodUF { get; set; }
        [ForeignKey("CodUF")]
        public UnidadeFederativa UnidadeFederativa { get; set; }
        public int? CodCidade { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
    }
}
