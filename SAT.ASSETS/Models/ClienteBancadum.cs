using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ClienteBancadum
    {
        public ClienteBancadum()
        {
            ClienteBancadaLista = new HashSet<ClienteBancadaListum>();
            OrcamentosFaturamentobkps = new HashSet<OrcamentosFaturamentobkp>();
        }

        [Key]
        public int CodClienteBancada { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(100)]
        public string NomeCliente { get; set; }
        [StringLength(100)]
        public string Apelido { get; set; }
        [Column("CNPJ_CGC")]
        [StringLength(20)]
        public string CnpjCgc { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Numero { get; set; }
        [StringLength(20)]
        public string Complem { get; set; }
        [StringLength(50)]
        public string Bairro { get; set; }
        [StringLength(20)]
        public string Telefone { get; set; }
        [Column("CEP")]
        [StringLength(20)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string Contato { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManut { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("ICMS", TypeName = "decimal(10, 4)")]
        public decimal? Icms { get; set; }
        public int? CodFormaPagto { get; set; }
        public int? CodTransportadora { get; set; }
        public byte? IndAtivo { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? Inflacao { get; set; }
        [StringLength(50)]
        public string InflacaoObs { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? Deflacao { get; set; }
        [StringLength(50)]
        public string DeflacaoObs { get; set; }
        public byte? CodTipoFrete { get; set; }
        public byte? IndOrcamento { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.ClienteBancada))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [InverseProperty(nameof(ClienteBancadaListum.CodClienteBancadaNavigation))]
        public virtual ICollection<ClienteBancadaListum> ClienteBancadaLista { get; set; }
        [InverseProperty(nameof(OrcamentosFaturamentobkp.CodClienteBancadaNavigation))]
        public virtual ICollection<OrcamentosFaturamentobkp> OrcamentosFaturamentobkps { get; set; }
    }
}
