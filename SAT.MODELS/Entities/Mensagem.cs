using System;

namespace SAT.MODELS.Entities
{
    public class Mensagem
    {
        public int CodMsg { get; set; }
        public string Conteudo { get; set; }
        public string CodUsuarioRemetente { get; set; }
        public Usuario UsuarioRemetente { get; set; }
        public string CodUsuarioDestinatario { get; set; }
        public Usuario UsuarioDestinatario { get; set; }
        public DateTime DataHoraCad { get; set; }
        public byte? IndLeitura { get; set; }
        public DateTime? DataHoraLeitura { get; set; }
    }
}