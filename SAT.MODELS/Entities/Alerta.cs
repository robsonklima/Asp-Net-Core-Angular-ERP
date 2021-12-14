using System.Collections.Generic;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class Alerta
    {
        public string Titulo { get; set; }
        public List<string> Descricao { get; set; }
        public string Tipo { get; set; }
    }
}
