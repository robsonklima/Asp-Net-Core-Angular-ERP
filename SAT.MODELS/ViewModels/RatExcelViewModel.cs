using SAT.MODELS.Helpers;
using System;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class RatExcelViewModel
    {
        public int Chamado { get; set; }
        public string Rat { get; set; }
        public string RelatoSolucao { get; set; }
        public string Tecnico { get; set; }
        public string Status { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string TipoServico { get; set; }
        public string Observacao { get; set; }

    }
}
