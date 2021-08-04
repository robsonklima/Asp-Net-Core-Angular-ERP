using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PecaFamilia
    {
        [Key]
        public int CodPecaFamilia { get; set; }
        public int CodPecaBase { get; set; }
        public string NomeFamilia { get; set; }
        public string DescFamilia { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
