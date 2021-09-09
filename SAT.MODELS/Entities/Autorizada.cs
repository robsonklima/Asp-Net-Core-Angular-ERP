using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Autorizada
    {
        [Key]
        public int? CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int? CodCidade { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public byte IndFilialPerto { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int CodTipoRota { get; set; }
        [ForeignKey("CodTipoRota")]
        public TipoRota TipoRota { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool? AtendePOS { get; set; }
    }
}
