using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class CheckinCheckout
    {
        [Key]
        public int CodCheckInCheckOut { get; set; }
        public string Tipo { get; set; }
        public string Modalidade { get; set; }
        public int? CodOS { get; set; }
        public int? CodRat { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CodUsuarioTecnico { get; set; }
        public string CodUsuarioPa { get; set; }
        public DateTime? DataHoraCadSmartphone { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string DistanciaLocalMetros { get; set; }
        public string DistanciaLocalDescricao { get; set; }
        public string DuracaoLocalSegundos { get; set; }
        public string DuracaoLocalDescricao { get; set; }
    }
}
