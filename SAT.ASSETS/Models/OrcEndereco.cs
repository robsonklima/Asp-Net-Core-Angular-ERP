using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcEndereco")]
    public partial class OrcEndereco
    {
        [Key]
        public int CodOrcEndereco { get; set; }
        public int? CodOrc { get; set; }
        [StringLength(255)]
        public string Filial { get; set; }
        [StringLength(255)]
        public string RazaoSocial { get; set; }
        [StringLength(255)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Rua { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [StringLength(255)]
        public string Cnpj { get; set; }
        [StringLength(255)]
        public string InscricaoEstadual { get; set; }
        [StringLength(255)]
        public string Responsavel { get; set; }
        [StringLength(255)]
        public string Numero { get; set; }
        [StringLength(255)]
        public string Bairro { get; set; }
        [StringLength(255)]
        public string NomeCidade { get; set; }
        [StringLength(255)]
        public string Uf { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Fone { get; set; }
        [StringLength(255)]
        public string Fax { get; set; }
        [StringLength(255)]
        public string NomeLocal { get; set; }
        [StringLength(255)]
        public string Agencia { get; set; }
        [StringLength(255)]
        public string CodigoOrdemServico { get; set; }
        [StringLength(255)]
        public string NumeroCodigoOrdemServicoCliente { get; set; }
        [StringLength(255)]
        public string NomeEquipamento { get; set; }
        [StringLength(255)]
        public string NumeroSerie { get; set; }
        [StringLength(255)]
        public string NumeroPatrimonio { get; set; }
        public byte? Tipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcEnderecos))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}
