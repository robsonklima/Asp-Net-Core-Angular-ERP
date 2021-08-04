using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Cidade
    {
        [Key]
        public int CodCidade { get; set; }
        public int CodUF { get; set; }
        [ForeignKey("CodUF")]
        public UnidadeFederativa UnidadeFederativa { get; set; }
        public int CodFilial { get; set; }
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }
        public byte? CodSlAParametroAdicional { get; set; }
        public string NomeCidade { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public byte IndAtivo { get; set; }
        public int IndConsulta { get; set; }
        public byte? IndCapital { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        [NotMapped]
        public double? LatitudeMetros { get; set; }
        [NotMapped]
        public double? LongitudeMetros { get; set; }
        public int? Regiao { get; set; }
        [NotMapped]
        public int? HorasRAcesso { get; set; }
        public int? CodRegiaoPOS { get; set; }
    }
}
