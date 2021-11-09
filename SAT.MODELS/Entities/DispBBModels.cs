using System;

namespace SAT.MODELS.Models
{
    public class DispRegiaoBBModel
    {
        public int CodRegiao { get; set; }
        public int CodCriticidade { get; set; }

        //Taa por região
        public int QTdr { get; set; }
        public int QtdOS { get; set; }
        public decimal ReceitaTotal { get; set; }
        public double MinDispTotal { get; set; }
        public double MinIndispTotal { get; set; }
        public double MetaRegiaoCriticidade { get; set; }

        // Calculados
        public double IndTaa => (MinIndispTotal / MinDispTotal) * 100;
        public double IndCr => IndTaa / QTdr;
        public double IndCrPercentual => 100 - IndCr;
        public double Desvio { get; set; }
        public decimal DesvioTabelado { get; set; }
        public decimal Rebate { get; set; }
    }
}