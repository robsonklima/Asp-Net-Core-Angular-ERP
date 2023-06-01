using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Transportadora
    {
        public Transportadora()
        {
            Clientes = new HashSet<Cliente>();
            Nfs = new HashSet<Nf>();
            SolicitacaoNfs = new HashSet<SolicitacaoNf>();
        }

        [Key]
        public int CodTransportadora { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(50)]
        public string NomeTransportadora { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string NomeResponsavel { get; set; }
        public byte? IndAtivo { get; set; }
        public int? Cidade { get; set; }
        [Column("SiglaUF")]
        public int? SiglaUf { get; set; }
        [StringLength(20)]
        public string Pais { get; set; }
        [StringLength(20)]
        public string Cep { get; set; }
        [StringLength(20)]
        public string Telefone1 { get; set; }
        [StringLength(20)]
        public string Telefone2 { get; set; }
        [StringLength(20)]
        public string Celular { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Site { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [StringLength(20)]
        public string ComplemEnd { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty("Transportadoras")]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [InverseProperty(nameof(Cliente.CodTransportadoraNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(Nf.CodTransportadoraNavigation))]
        public virtual ICollection<Nf> Nfs { get; set; }
        [InverseProperty(nameof(SolicitacaoNf.CodTransportadoraNavigation))]
        public virtual ICollection<SolicitacaoNf> SolicitacaoNfs { get; set; }
    }
}
