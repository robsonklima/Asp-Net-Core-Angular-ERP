using System;

namespace SAT.MODELS.Entities
{
    public class DespesaCartaoCombustivelTecnico
    {
        public int CodDespesaCartaoCombustivelTecnico { get; set; }
        public int CodDespesaCartaoCombustivel { get; set; }
        public int CodTecnico { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DespesaCartaoCombustivel DespesaCartaoCombustivel { get; set; }
    }
}