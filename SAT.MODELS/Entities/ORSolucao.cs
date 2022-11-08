using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class ORSolucao
    {
        public int CodSolucao { get; set; }
        public string Descricao { get; set; }
        public int CodSolucaoLab { get; set; }
        public int? IndAtivo { get; set; }
    }
}