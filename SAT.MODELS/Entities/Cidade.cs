using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Cidade
    {
        public int CodCidade { get; set; }
        public int CodUF { get; set; }
        public UnidadeFederativa UnidadeFederativa { get; set; }
        public int CodFilial { get; set; }
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
        public double? LatitudeMetros { get; set; }
        public double? LongitudeMetros { get; set; }
        public int? Regiao { get; set; }
        public int? Horas_RAcesso { get; set; }
        public int? CodRegiaoPOS { get; set; }
    }
}
