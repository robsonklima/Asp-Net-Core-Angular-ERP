using System;

namespace SAT.MODELS.Views
{
    public class ViewDashboardPecasFaltantes
    {
        public string NomeUsuario { get; set; }
        public string NomeFilial { get; set; }
        public DateTime? DataFaltante { get; set; }
        public int? Qtd { get; set; }
    }
}
