using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("Transportadoras")]
    public class Transportadora
    {
        [Key]
        public int? CodTransportadora { get; set; }
        public int? CodCidade { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeTransportadora { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string NomeResponsavel { get; set; }
        public byte? IndAtivo { get; set; }
        public int? SiglaUf { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string NumeroEnd { get; set; }
        public string ComplemEnd { get; set; }
    }
}
