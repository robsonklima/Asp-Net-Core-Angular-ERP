using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Tecnico")]
    public partial class Tecnico
    {
        public Tecnico()
        {
            DespesaAdiantamentos = new HashSet<DespesaAdiantamento>();
            DespesaAluguelCarros = new HashSet<DespesaAluguelCarro>();
            DespesaPeriodoTecnicos = new HashSet<DespesaPeriodoTecnico>();
            DespesaTentativaKms = new HashSet<DespesaTentativaKm>();
            Despesas = new HashSet<Despesa>();
            FecharOspos = new HashSet<FecharOspo>();
            OsbancadaPecasTransfs = new HashSet<OsbancadaPecasTransf>();
            PlantaoTecnicos = new HashSet<PlantaoTecnico>();
            TecnicoConta = new HashSet<TecnicoContum>();
            TecnicoVeiculos = new HashSet<TecnicoVeiculo>();
        }

        [Key]
        public int CodTecnico { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public int CodTipoRota { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(20)]
        public string Apelido { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataNascimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAdmissao { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        [Column("RG")]
        [StringLength(20)]
        public string Rg { get; set; }
        [Column("CEP")]
        [StringLength(20)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(50)]
        public string EnderecoComplemento { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(100)]
        public string EnderecoCoordenadas { get; set; }
        [StringLength(60)]
        public string BairroCoordenadas { get; set; }
        [StringLength(60)]
        public string CidadeCoordenadas { get; set; }
        [Column("UFCoordenadas")]
        [StringLength(50)]
        public string Ufcoordenadas { get; set; }
        [StringLength(50)]
        public string PaisCoordenadas { get; set; }
        [StringLength(60)]
        public string Bairro { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(40)]
        public string Fone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column("NumCREA")]
        [StringLength(30)]
        public string NumCrea { get; set; }
        public byte? IndTecnicoBancada { get; set; }
        public byte? IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(16)]
        public string FoneParticular { get; set; }
        [StringLength(16)]
        public string FonePerto { get; set; }
        [StringLength(40)]
        public string SimCardMobile { get; set; }
        [Column("IndPA")]
        public int? IndPa { get; set; }
        [Column("TrackerID")]
        [StringLength(14)]
        public string TrackerId { get; set; }
        public int? CodSimCard { get; set; }
        [Column("CPFLogix")]
        [StringLength(20)]
        public string Cpflogix { get; set; }
        [Column("indFerias")]
        public int IndFerias { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodDespesaCartaoCombustivel { get; set; }
        public int? CodFrotaCobrancaGaragem { get; set; }
        public int? CodFrotaFinalidadeUso { get; set; }
        [Column("CNH")]
        [StringLength(50)]
        public string Cnh { get; set; }
        [Column("CNHCategorias")]
        [StringLength(50)]
        public string Cnhcategorias { get; set; }
        [StringLength(50)]
        public string FinalidadesUso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtFeriasInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtFeriasFim { get; set; }
        [Column("CNHValidade")]
        [StringLength(50)]
        public string Cnhvalidade { get; set; }

        [ForeignKey(nameof(CodTipoRota))]
        [InverseProperty(nameof(TipoRotum.Tecnicos))]
        public virtual TipoRotum CodTipoRotaNavigation { get; set; }
        [InverseProperty(nameof(DespesaAdiantamento.CodTecnicoNavigation))]
        public virtual ICollection<DespesaAdiantamento> DespesaAdiantamentos { get; set; }
        [InverseProperty(nameof(DespesaAluguelCarro.CodTecnicoNavigation))]
        public virtual ICollection<DespesaAluguelCarro> DespesaAluguelCarros { get; set; }
        [InverseProperty(nameof(DespesaPeriodoTecnico.CodTecnicoNavigation))]
        public virtual ICollection<DespesaPeriodoTecnico> DespesaPeriodoTecnicos { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodTecnicoNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKms { get; set; }
        [InverseProperty(nameof(Despesa.CodTecnicoNavigation))]
        public virtual ICollection<Despesa> Despesas { get; set; }
        [InverseProperty(nameof(FecharOspo.CodTecnicoNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(OsbancadaPecasTransf.CodTecnicoNavigation))]
        public virtual ICollection<OsbancadaPecasTransf> OsbancadaPecasTransfs { get; set; }
        [InverseProperty(nameof(PlantaoTecnico.CodTecnicoNavigation))]
        public virtual ICollection<PlantaoTecnico> PlantaoTecnicos { get; set; }
        [InverseProperty(nameof(TecnicoContum.CodTecnicoNavigation))]
        public virtual ICollection<TecnicoContum> TecnicoConta { get; set; }
        [InverseProperty(nameof(TecnicoVeiculo.CodTecnicoNavigation))]
        public virtual ICollection<TecnicoVeiculo> TecnicoVeiculos { get; set; }
    }
}
