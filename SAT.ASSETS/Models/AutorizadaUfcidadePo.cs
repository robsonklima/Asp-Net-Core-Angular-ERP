using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AutorizadaUFCidadePOS")]
    public partial class AutorizadaUfcidadePo
    {
        [Key]
        [Column("CodAutorizadaUFCidadePOS")]
        public int CodAutorizadaUfcidadePos { get; set; }
        public int CodAutorizada { get; set; }
        public int CodCliente { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int? CodCidade { get; set; }
        public int CodRegiao { get; set; }
        public int CodFilial { get; set; }

        [ForeignKey(nameof(CodAutorizada))]
        [InverseProperty(nameof(Autorizadum.AutorizadaUfcidadePos))]
        public virtual Autorizadum CodAutorizadaNavigation { get; set; }
        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.AutorizadaUfcidadePos))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.AutorizadaUfcidadePos))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.AutorizadaUfcidadePos))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodRegiao))]
        [InverseProperty(nameof(Regiao.AutorizadaUfcidadePos))]
        public virtual Regiao CodRegiaoNavigation { get; set; }
        [ForeignKey(nameof(CodUf))]
        [InverseProperty(nameof(Uf.AutorizadaUfcidadePos))]
        public virtual Uf CodUfNavigation { get; set; }
    }
}
