using System;
namespace SAT.MODELS.Entities
{
    public class DocumentoSistema
    {
        public int CodDocumentoSistema { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Categoria { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public Usuario UsuarioManut { get; set; }
        public byte? IndAtivo { get; set; }
    }
}