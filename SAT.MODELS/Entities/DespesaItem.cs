using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DespesaItem
    {
        [Key]
        public int CodDespesaItem { get; set; }
        public int CodDespesa { get; set; }
        public int CodDespesaTipo { get; set; }
        [ForeignKey("CodDespesaTipo")]
        public DespesaTipo DespesaTipo { get; set; }
        public int CodDespesaConfiguracao { get; set; }
        public int? SequenciaDespesaKm { get; set; }
        public string NumNF { get; set; }
        public decimal? DespesaValor { get; set; }
        public string EnderecoOrigem { get; set; }
        public string NumOrigem { get; set; }
        public string BairroOrigem { get; set; }
        public int? CodCidadeOrigem { get; set; }
        public string EnderecoOrigemWebraska { get; set; }
        public string NumOrigemWebraska { get; set; }
        public string BairroOrigemWebraska { get; set; }
        public string NomeCidadeOrigemWebraska { get; set; }
        public string SiglaUFOrigemWebraska { get; set; }
        public string SiglaPaisOrigemWebraska { get; set; }
        public byte IndResidenciaOrigem { get; set; }
        public byte IndHotelOrigem { get; set; }
        public string EnderecoDestino { get; set; }
        public string NumDestino { get; set; }
        public string BairroDestino { get; set; }
        public int? CodCidadeDestino { get; set; }
        public string EnderecoDestinoWebraska { get; set; }
        public string NumDestinoWebraska { get; set; }
        public string BairroDestinoWebraska { get; set; }
        public string NomeCidadeDestinoWebraska { get; set; }
        public string SiglaUFDestinoWebraska { get; set; }
        public string SiglaPaisDestinoWebraska { get; set; }
        public byte IndResidenciaDestino { get; set; }
        public byte IndHotelDestino { get; set; }
        public decimal? KmPrevisto { get; set; }
        public int? KmPercorrido { get; set; }
        public string TentativaKM { get; set; }
        public string Obs { get; set; }
        public string ObsReprovacao { get; set; }
        public int? CodDespesaItemAlerta { get; set; }
        public byte IndWebrascaIndisponivel { get; set; }
        public byte IndReprovado { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string LatitudeHotel { get; set; }
        public string LongitudeHotel { get; set; }

        [ForeignKey("CodCidadeOrigem")]
        public virtual Cidade CidadeOrigem { get; set; }

        [ForeignKey("CodCidadeDestino")]
        public virtual Cidade CidadeDestino { get; set; }
    }
}