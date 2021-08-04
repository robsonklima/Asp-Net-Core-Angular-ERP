using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Contrato
    {
        [Key]
        public int CodContrato { get; set; }
        public int? CodContratoPai { get; set; }
        public int? CodCliente { get; set; }
        public int? CodTipoContrato { get; set; }
        public string Cnpj { get; set; }
        public string NroContrato { get; set; }
        public string NomeContrato { get; set; }
        public DateTime? DataContrato { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public DateTime? DataInicioPeriodoReajuste { get; set; }
        public DateTime? DataFimPeriodoReajuste { get; set; }
        public string NomeResponsavelPerto { get; set; }
        public string NomeResponsavelCliente { get; set; }
        public string ObjetoContrato { get; set; }
        public decimal? ValTotalContrato { get; set; }
        public short? NumMinReincidencia { get; set; }
        public int? KmMinimoAdicional { get; set; }
        public int? KmAdicionalHora { get; set; }
        public int Mtbfnominal { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndPermitePecaGenerica { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public DateTime? HoraDispInicio { get; set; }
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        public int? CodSLA { get; set; }
        public short? NumDiasSubstEquip { get; set; }
        public int? CodEmpresa { get; set; }
        public DateTime? DataReajuste { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string CodUsuarioCancelamento { get; set; }
        public string MotivoCancelamento { get; set; }
        public string NumeroEnd { get; set; }
        public string ComplemEnd { get; set; }
        public string EnderecoCobranca { get; set; }
        public string BairroCobranca { get; set; }
        public string CidadeCobranca { get; set; }
        public string SiglaUFcobranca { get; set; }
        public string CepCobranca { get; set; }
        public string TelefoneCobranca { get; set; }
        public string Faxcobranca { get; set; }
        public byte? IndGarantia { get; set; }
        public byte? IndHerenca { get; set; }
        public decimal? PercReajuste { get; set; }
        public byte? IndPermitePecaEspecifica { get; set; }
        public string SemCobertura { get; set; }
    }
}
