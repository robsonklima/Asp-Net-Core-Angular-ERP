using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Localizacao
    {
        [Key]
        public int? CodLocalizacao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
