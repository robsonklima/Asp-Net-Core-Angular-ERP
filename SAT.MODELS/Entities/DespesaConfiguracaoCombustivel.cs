using System;

namespace SAT.MODELS.Entities
{
    public class DespesaConfiguracaoCombustivel
    {
        public int CodDespesaConfiguracaoCombustivel { get; set; }
        public int? CodFilial { get; set; }
       // public Filial Filial { get; set; }
        public int? CodUf { get; set; }
       // public UnidadeFederativa UnidadeFederativa { get; set; }
        public double? PrecoLitro { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
        //public Usuario Usuario { get; set; }
    }
}