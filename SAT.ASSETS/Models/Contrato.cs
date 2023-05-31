using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Contrato")]
    [Index(nameof(NomeContrato), Name = "IX_NomeContrato", IsUnique = true)]
    public partial class Contrato
    {
        public Contrato()
        {
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            ClientePecas = new HashSet<ClientePeca>();
            ClienteSemLista = new HashSet<ClienteSemListum>();
            ContratoEquipamentos = new HashSet<ContratoEquipamento>();
            ContratoPecas = new HashSet<ContratoPeca>();
            ContratoReajustes = new HashSet<ContratoReajuste>();
            ContratoSlas = new HashSet<ContratoSla>();
            HistoricoClientePecas = new HashSet<HistoricoClientePeca>();
            InstalLotes = new HashSet<InstalLote>();
            InstalPagtos = new HashSet<InstalPagto>();
            InstalPleitos = new HashSet<InstalPleito>();
            LocalEnvioNffaturamentoVinculados = new HashSet<LocalEnvioNffaturamentoVinculado>();
            LocalEnvioNffaturamentos = new HashSet<LocalEnvioNffaturamento>();
            ValorServicos = new HashSet<ValorServico>();
        }

        [Key]
        public int CodContrato { get; set; }
        public int? CodContratoPai { get; set; }
        public int? CodCliente { get; set; }
        public int? CodTipoContrato { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(50)]
        public string NroContrato { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAssinatura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicioVigencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimVigencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicioPeriodoReajuste { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimPeriodoReajuste { get; set; }
        [StringLength(50)]
        public string NomeResponsavelPerto { get; set; }
        [StringLength(50)]
        public string NomeResponsavelCliente { get; set; }
        [StringLength(2000)]
        public string ObjetoContrato { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValTotalContrato { get; set; }
        public short? NumMinReincidencia { get; set; }
        public int? KmMinimoAdicional { get; set; }
        public int? KmAdicionalHora { get; set; }
        [Column("MTBFNominal")]
        public int Mtbfnominal { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndPermitePecaGenerica { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public short? NumDiasSubstEquip { get; set; }
        public int? CodEmpresa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataReajuste { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCancelamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }
        [StringLength(2000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [StringLength(20)]
        public string ComplemEnd { get; set; }
        [StringLength(100)]
        public string EnderecoCobranca { get; set; }
        [StringLength(20)]
        public string BairroCobranca { get; set; }
        [StringLength(20)]
        public string CidadeCobranca { get; set; }
        [Column("SiglaUFCobranca")]
        [StringLength(20)]
        public string SiglaUfcobranca { get; set; }
        [Column("CEPCobranca")]
        [StringLength(20)]
        public string Cepcobranca { get; set; }
        [StringLength(20)]
        public string TelefoneCobranca { get; set; }
        [Column("FAXCobranca")]
        [StringLength(20)]
        public string Faxcobranca { get; set; }
        public byte? IndGarantia { get; set; }
        public byte? IndHerenca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercReajuste { get; set; }
        public byte? IndPermitePecaEspecifica { get; set; }
        public string SemCobertura { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.Contratos))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodSla))]
        [InverseProperty(nameof(Sla.Contratos))]
        public virtual Sla CodSlaNavigation { get; set; }
        [ForeignKey(nameof(CodTipoContrato))]
        [InverseProperty(nameof(TipoContrato.Contratos))]
        public virtual TipoContrato CodTipoContratoNavigation { get; set; }
        [InverseProperty(nameof(AutorizadaRepasse.CodContratoNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(ClientePeca.CodContratoNavigation))]
        public virtual ICollection<ClientePeca> ClientePecas { get; set; }
        [InverseProperty(nameof(ClienteSemListum.CodContratoNavigation))]
        public virtual ICollection<ClienteSemListum> ClienteSemLista { get; set; }
        [InverseProperty(nameof(ContratoEquipamento.CodContratoNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentos { get; set; }
        [InverseProperty(nameof(ContratoPeca.CodContratoNavigation))]
        public virtual ICollection<ContratoPeca> ContratoPecas { get; set; }
        [InverseProperty(nameof(ContratoReajuste.CodContratoNavigation))]
        public virtual ICollection<ContratoReajuste> ContratoReajustes { get; set; }
        [InverseProperty(nameof(ContratoSla.CodContratoNavigation))]
        public virtual ICollection<ContratoSla> ContratoSlas { get; set; }
        [InverseProperty(nameof(HistoricoClientePeca.CodContratoNavigation))]
        public virtual ICollection<HistoricoClientePeca> HistoricoClientePecas { get; set; }
        [InverseProperty(nameof(InstalLote.CodContratoNavigation))]
        public virtual ICollection<InstalLote> InstalLotes { get; set; }
        [InverseProperty(nameof(InstalPagto.CodContratoNavigation))]
        public virtual ICollection<InstalPagto> InstalPagtos { get; set; }
        [InverseProperty(nameof(InstalPleito.CodContratoNavigation))]
        public virtual ICollection<InstalPleito> InstalPleitos { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamentoVinculado.CodContratoNavigation))]
        public virtual ICollection<LocalEnvioNffaturamentoVinculado> LocalEnvioNffaturamentoVinculados { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodContratoNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentos { get; set; }
        [InverseProperty(nameof(ValorServico.CodContratoNavigation))]
        public virtual ICollection<ValorServico> ValorServicos { get; set; }
    }
}
