using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities {
    public class Ticket
    {
        [Key]
        public int CodTicket { get; set; }
        public string CodUsuario { get; set; }
        public int CodModulo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CodClassificacao { get; set; }
        public int CodStatus { get; set; }
        public DateTime DataCadastro { get; set; }
        public string UsuarioManut { get; set; }
        public DateTime DataManut { get; set; }
        public DateTime DataFechamento { get; set; }
        public int CodPrioridade { get; set; }
    }
}