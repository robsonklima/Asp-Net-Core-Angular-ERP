using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Chamado
    {
        public int CodChamado { get; set; }
        [Key]
        public int? CodOS { get; set; }
        public string NumeroOSCliente { get; set; }
        public DateTime DataHoraAbertura { get; set; }
        public string Classificacao { get; set; }
        public string FoneOrigem { get; set; }
        public string CnpjCpf { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string EnderecoSolicitante { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string NomeContato { get; set; }
        public string FoneContato { get; set; }
        public string Rede { get; set; }
        public string Estabelecimento { get; set; }
        public string Terminal { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public string CodUsuarioOS { get; set; }
        public DateTime DataHoraOS { get; set; }
        public string CodUsuarioAtendimento { get; set; }
        public string DescricaoAtendimento { get; set; }
        public DateTime HorarioInicioAtendimento { get; set; }
        public DateTime HorarioFimAtendimento { get; set; }
        public int CodStatus { get; set; }
        public int CodMotivoCancelamento { get; set; }
        public int CodDefeitoPOS { get; set; }
        public int CodPosto { get; set; }
        public int CodCliente { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodEquipContrato { get; set; }
        public string Linha { get; set; }
        public string ComplementoDefeito { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public bool ExigeVisitaTecnica { get; set; }
        public int CodOperadoraTelefonia { get; set; }
        public string Versao { get; set; }
    }
}