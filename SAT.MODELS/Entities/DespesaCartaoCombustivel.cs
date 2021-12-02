using System;

namespace SAT.MODELS.Entities
{
    public class DespesaCartaoCombustivel
    {
        public int CodDespesaCartaoCombustivel { get; set; }
        public string Numero { get; set; }
        public string Carro { get; set; }
        public string Placa { get; set; }
        public string Ano { get; set; }
        public string Cor { get; set; }
        public string Combustivel { get; set; }
        public string CodUsuarioCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public int? IndAtivo { get; set; }
        public TicketLogUsuarioCartaoPlaca TicketLogUsuarioCartaoPlaca { get; set; }
    }
}
