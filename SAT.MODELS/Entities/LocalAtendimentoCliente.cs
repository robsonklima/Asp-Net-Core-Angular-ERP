using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class LocalAtendimentoCliente
    {
        public int Codigo { get; set; }
        public string NumAgencia { get; set; }
        public string DCPosto { get; set; }
        public string Endereco { get; set; }
        public List<EquipamentoCliente> Equipamentos { get; set; }
    }
}
