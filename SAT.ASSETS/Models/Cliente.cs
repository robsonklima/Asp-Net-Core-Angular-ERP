using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Cliente")]
    [Index(nameof(NumBanco), Name = "IX_Cliente", IsUnique = true)]
    public partial class Cliente
    {
        public Cliente()
        {
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            CidadeDeParaClientes = new HashSet<CidadeDeParaCliente>();
            ClientePecas = new HashSet<ClientePeca>();
            ClienteSemLista = new HashSet<ClienteSemListum>();
            ClienteUsuarios = new HashSet<ClienteUsuario>();
            Contratos = new HashSet<Contrato>();
            ControleReincidencia = new HashSet<ControleReincidencium>();
            FeriadosPos = new HashSet<FeriadosPo>();
            FinanceiroPosprestadoras = new HashSet<FinanceiroPosprestadora>();
            HistoricoClientePecas = new HashSet<HistoricoClientePeca>();
            LocalEnvioNffaturamentos = new HashSet<LocalEnvioNffaturamento>();
            Osatendida = new HashSet<Osatendida>();
            PedidoInvoices = new HashSet<PedidoInvoice>();
            PlantaoTecnicoClientes = new HashSet<PlantaoTecnicoCliente>();
            ProdutoClientes = new HashSet<ProdutoCliente>();
            SolicitacaoNfs = new HashSet<SolicitacaoNf>();
            TipoComunicacaoDeParas = new HashSet<TipoComunicacaoDePara>();
            ValorServicos = new HashSet<ValorServico>();
        }

        [Key]
        public int CodCliente { get; set; }
        public int? CodFormaPagto { get; set; }
        public int? CodMoeda { get; set; }
        public int? CodTipoFrete { get; set; }
        public int? CodPecaLista { get; set; }
        public int? CodTransportadora { get; set; }
        public int? CodCidade { get; set; }
        [Required]
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Required]
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(30)]
        public string Bairro { get; set; }
        [StringLength(40)]
        public string Fone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Site { get; set; }
        [StringLength(300)]
        public string Observacao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Inflacao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Deflacao { get; set; }
        [Column("PercICMS", TypeName = "decimal(10, 2)")]
        public decimal? PercIcms { get; set; }
        public byte? IndHabilitaVendaPecas { get; set; }
        public byte IndPecaListaSomente { get; set; }
        public byte? IndRevisao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? TotalEquipAtivos { get; set; }
        public int? TotalEquipDeastivados { get; set; }
        public int? TotalEquip { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [StringLength(1000)]
        public string ObsCliente { get; set; }
        [StringLength(50)]
        public string ComplemEnd { get; set; }
        [StringLength(30)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        [StringLength(20)]
        public string Telefone1 { get; set; }
        [StringLength(20)]
        public string Telefone2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        public int? CodTipoMercado { get; set; }
        [StringLength(20)]
        public string CodUsuarioCoordenadorContrato { get; set; }
        [Column("ICMSLab", TypeName = "decimal(10, 4)")]
        public decimal? Icmslab { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? InflacaoLab { get; set; }
        [Column("InflacaoOBSLab")]
        [StringLength(50)]
        public string InflacaoObslab { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? DeflacaoLab { get; set; }
        [Column("DeflacaoOBSLab")]
        [StringLength(50)]
        public string DeflacaoObslab { get; set; }
        public int? CodPecaListaLab { get; set; }
        public int? CodFormaPagtoLab { get; set; }
        public int? CodTipoFreteLab { get; set; }
        public int? CodTransportadoraLab { get; set; }
        public byte? IndOrcamentoLab { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty("Clientes")]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodFormaPagto))]
        [InverseProperty(nameof(FormaPagto.Clientes))]
        public virtual FormaPagto CodFormaPagtoNavigation { get; set; }
        [ForeignKey(nameof(CodMoeda))]
        [InverseProperty(nameof(Moedum.Clientes))]
        public virtual Moedum CodMoedaNavigation { get; set; }
        [ForeignKey(nameof(CodPecaLista))]
        [InverseProperty(nameof(PecaListum.Clientes))]
        public virtual PecaListum CodPecaListaNavigation { get; set; }
        [ForeignKey(nameof(CodTipoFrete))]
        [InverseProperty(nameof(TipoFrete.Clientes))]
        public virtual TipoFrete CodTipoFreteNavigation { get; set; }
        [ForeignKey(nameof(CodTransportadora))]
        [InverseProperty(nameof(Transportadora.Clientes))]
        public virtual Transportadora CodTransportadoraNavigation { get; set; }
        [InverseProperty(nameof(AutorizadaRepasse.CodClienteNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodClienteNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(CidadeDeParaCliente.CodClienteNavigation))]
        public virtual ICollection<CidadeDeParaCliente> CidadeDeParaClientes { get; set; }
        [InverseProperty(nameof(ClientePeca.CodClienteNavigation))]
        public virtual ICollection<ClientePeca> ClientePecas { get; set; }
        [InverseProperty(nameof(ClienteSemListum.CodClienteNavigation))]
        public virtual ICollection<ClienteSemListum> ClienteSemLista { get; set; }
        [InverseProperty(nameof(ClienteUsuario.CodClienteNavigation))]
        public virtual ICollection<ClienteUsuario> ClienteUsuarios { get; set; }
        [InverseProperty(nameof(Contrato.CodClienteNavigation))]
        public virtual ICollection<Contrato> Contratos { get; set; }
        [InverseProperty(nameof(ControleReincidencium.CodClienteNavigation))]
        public virtual ICollection<ControleReincidencium> ControleReincidencia { get; set; }
        [InverseProperty(nameof(FeriadosPo.CodClienteNavigation))]
        public virtual ICollection<FeriadosPo> FeriadosPos { get; set; }
        [InverseProperty(nameof(FinanceiroPosprestadora.CodClienteNavigation))]
        public virtual ICollection<FinanceiroPosprestadora> FinanceiroPosprestadoras { get; set; }
        [InverseProperty(nameof(HistoricoClientePeca.CodClienteNavigation))]
        public virtual ICollection<HistoricoClientePeca> HistoricoClientePecas { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodClienteNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentos { get; set; }
        [InverseProperty("CodClienteNavigation")]
        public virtual ICollection<Osatendida> Osatendida { get; set; }
        [InverseProperty(nameof(PedidoInvoice.CodClienteDeliveryNavigation))]
        public virtual ICollection<PedidoInvoice> PedidoInvoices { get; set; }
        [InverseProperty(nameof(PlantaoTecnicoCliente.CodClienteNavigation))]
        public virtual ICollection<PlantaoTecnicoCliente> PlantaoTecnicoClientes { get; set; }
        [InverseProperty(nameof(ProdutoCliente.CodClienteNavigation))]
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        [InverseProperty(nameof(SolicitacaoNf.CodClienteNavigation))]
        public virtual ICollection<SolicitacaoNf> SolicitacaoNfs { get; set; }
        [InverseProperty(nameof(TipoComunicacaoDePara.CodClienteNavigation))]
        public virtual ICollection<TipoComunicacaoDePara> TipoComunicacaoDeParas { get; set; }
        [InverseProperty(nameof(ValorServico.CodClienteNavigation))]
        public virtual ICollection<ValorServico> ValorServicos { get; set; }
    }
}
