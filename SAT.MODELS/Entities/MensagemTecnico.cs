using System;

namespace SAT.MODELS.Entities
{
    public class MensagemTecnico    
    {
        public int CodMensagemTecnico { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string CodUsuarioDestinatario { get; set; }
        public Usuario UsuarioDestinatario { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public byte? IndLeitura { get; set; }
        public DateTime? DataHoraLeitura { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
