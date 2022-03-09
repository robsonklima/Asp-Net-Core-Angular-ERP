using System;

namespace SAT.MODELS.Entities
{
    public class PecaLista
    {
        public int CodPecaLista { get; set; }
        public string NomePecaLista { get; set; }
        public string DescPecaLista { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
