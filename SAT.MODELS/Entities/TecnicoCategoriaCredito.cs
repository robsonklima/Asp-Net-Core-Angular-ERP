using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class TecnicoCategoriaCredito
    {
        public int CodTecnico { get; set; }
        public TecnicoCategoriaCreditoEnum CategoriaCredito { get; set; }
        public double? Media { get; set; }
        public double? Valor { get; set; }

    }
}