using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PerfilSetor
    {
        [Key]
        public int CodPerfilSetor { get; set; }
        public int CodSetor { get; set; }
        public int CodPerfil { get; set; }
        public int IndAtivo { get; set; }
        public Setor Setor { get; set; }
        public Perfil Perfil { get; set; }
    }
}
