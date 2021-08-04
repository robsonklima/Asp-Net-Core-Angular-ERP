using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("UF")]
    public class UnidadeFederativa
    {
        [Key]
        public int CodUF { get; set; }
        public int CodPais { get; set; }
        [ForeignKey("CodPais")]
        public Pais Pais { get; set; }
        public string SiglaUF { get; set; }
        public string NomeUF { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
