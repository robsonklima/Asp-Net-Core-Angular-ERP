using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Peca")]
    [Index(nameof(CodMagnus), Name = "IX_Peca_CodMagnus", IsUnique = true)]
    public partial class Peca
    {
        public Peca()
        {
            BancadaListaPecas = new HashSet<BancadaListaPeca>();
            ClientePecas = new HashSet<ClientePeca>();
            ConjuntoConjuntos = new HashSet<ConjuntoConjunto>();
            ConjuntoPecas = new HashSet<ConjuntoPeca>();
            Conjuntos = new HashSet<Conjunto>();
            ContratoPecas = new HashSet<ContratoPeca>();
            InverseCodPecaSubstituicaoNavigation = new HashSet<Peca>();
            OrcamentosPecas = new HashSet<OrcamentosPeca>();
            OsbancadaPecasAplics = new HashSet<OsbancadaPecasAplic>();
            PecaFamilia = new HashSet<PecaFamilium>();
            PecaKitCodPecaKitNavigations = new HashSet<PecaKit>();
            PecaKitCodPecaNavigations = new HashSet<PecaKit>();
            PecaListaPecas = new HashSet<PecaListaPeca>();
            PecaRe5114s = new HashSet<PecaRe5114>();
            PedidoNfpecas = new HashSet<PedidoNfpeca>();
            PedidoPecaLotes = new HashSet<PedidoPecaLote>();
            PedidoPecas = new HashSet<PedidoPeca>();
            RatdetalhesPecas = new HashSet<RatdetalhesPeca>();
        }

        [Key]
        public int CodPeca { get; set; }
        [Required]
        [StringLength(24)]
        public string CodMagnus { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int CodPecaStatus { get; set; }
        public int? CodTraducao { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCusto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValCustoDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCustoEuro { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPecaDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaEuro { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaAssistencia { get; set; }
        [Column("ValIPIAssistencia", TypeName = "decimal(10, 2)")]
        public decimal? ValIpiassistencia { get; set; }
        [Column("Descr_Ingles")]
        [StringLength(80)]
        public string DescrIngles { get; set; }
        public int IndObrigRastreabilidade { get; set; }
        public int IndValorFixo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAtualizacaoValor { get; set; }
        public int IsValorAtualizado { get; set; }
        [Column("NCM")]
        [StringLength(10)]
        public string Ncm { get; set; }
        public byte? ListaBackup { get; set; }
        [StringLength(50)]
        public string DtObsoleto { get; set; }
        [Column("UtilizadoDSS")]
        public byte? UtilizadoDss { get; set; }
        public byte? ItemLogix { get; set; }
        public int? HierarquiaPesquisa { get; set; }
        public double? IndiceDeTroca { get; set; }
        public byte? KitTecnico { get; set; }
        [Column("QTDPecaKitTecnico")]
        public int? QtdpecaKitTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataIntegracaoLogix { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtualizacao { get; set; }

        [ForeignKey(nameof(CodPecaFamilia))]
        [InverseProperty(nameof(PecaFamilium.Pecas))]
        public virtual PecaFamilium CodPecaFamiliaNavigation { get; set; }
        [ForeignKey(nameof(CodPecaStatus))]
        [InverseProperty(nameof(PecaStatus.Pecas))]
        public virtual PecaStatus CodPecaStatusNavigation { get; set; }
        [ForeignKey(nameof(CodPecaSubstituicao))]
        [InverseProperty(nameof(Peca.InverseCodPecaSubstituicaoNavigation))]
        public virtual Peca CodPecaSubstituicaoNavigation { get; set; }
        [InverseProperty(nameof(BancadaListaPeca.CodPecaNavigation))]
        public virtual ICollection<BancadaListaPeca> BancadaListaPecas { get; set; }
        [InverseProperty(nameof(ClientePeca.CodPecaNavigation))]
        public virtual ICollection<ClientePeca> ClientePecas { get; set; }
        [InverseProperty(nameof(ConjuntoConjunto.CodPecaAssociadaNavigation))]
        public virtual ICollection<ConjuntoConjunto> ConjuntoConjuntos { get; set; }
        [InverseProperty(nameof(ConjuntoPeca.CodPecaNavigation))]
        public virtual ICollection<ConjuntoPeca> ConjuntoPecas { get; set; }
        [InverseProperty(nameof(Conjunto.CodPecaBaseNavigation))]
        public virtual ICollection<Conjunto> Conjuntos { get; set; }
        [InverseProperty(nameof(ContratoPeca.CodPecaNavigation))]
        public virtual ICollection<ContratoPeca> ContratoPecas { get; set; }
        [InverseProperty(nameof(Peca.CodPecaSubstituicaoNavigation))]
        public virtual ICollection<Peca> InverseCodPecaSubstituicaoNavigation { get; set; }
        [InverseProperty(nameof(OrcamentosPeca.CodPecaNavigation))]
        public virtual ICollection<OrcamentosPeca> OrcamentosPecas { get; set; }
        [InverseProperty(nameof(OsbancadaPecasAplic.CodPecaNavigation))]
        public virtual ICollection<OsbancadaPecasAplic> OsbancadaPecasAplics { get; set; }
        [InverseProperty(nameof(PecaFamilium.CodPecaBaseNavigation))]
        public virtual ICollection<PecaFamilium> PecaFamilia { get; set; }
        [InverseProperty(nameof(PecaKit.CodPecaKitNavigation))]
        public virtual ICollection<PecaKit> PecaKitCodPecaKitNavigations { get; set; }
        [InverseProperty(nameof(PecaKit.CodPecaNavigation))]
        public virtual ICollection<PecaKit> PecaKitCodPecaNavigations { get; set; }
        [InverseProperty(nameof(PecaListaPeca.CodPecaNavigation))]
        public virtual ICollection<PecaListaPeca> PecaListaPecas { get; set; }
        [InverseProperty(nameof(PecaRe5114.CodPecaNavigation))]
        public virtual ICollection<PecaRe5114> PecaRe5114s { get; set; }
        [InverseProperty(nameof(PedidoNfpeca.CodPecaNavigation))]
        public virtual ICollection<PedidoNfpeca> PedidoNfpecas { get; set; }
        [InverseProperty(nameof(PedidoPecaLote.CodPecaNavigation))]
        public virtual ICollection<PedidoPecaLote> PedidoPecaLotes { get; set; }
        [InverseProperty(nameof(PedidoPeca.CodPecaNavigation))]
        public virtual ICollection<PedidoPeca> PedidoPecas { get; set; }
        [InverseProperty(nameof(RatdetalhesPeca.CodPecaNavigation))]
        public virtual ICollection<RatdetalhesPeca> RatdetalhesPecas { get; set; }
    }
}
