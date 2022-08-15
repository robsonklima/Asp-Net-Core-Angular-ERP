using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class ContratoServico
    {
        public int CodContratoServico { get; set; }
        public int CodContrato { get; set; }
        public int CodServico { get; set; }
        [ForeignKey("CodServico")]
        public TipoServico TipoServico { get; set; }
        public int? CodSLA { get; set; }
       [ForeignKey("CodSLA")]
       public AcordoNivelServico AcordoNivelServico { get; set; }
        public int CodTipoEquip { get; set; }
        [ForeignKey("CodTipoEquip")]
        public TipoEquipamento TipoEquipamento { get; set; }
        public int CodGrupoEquip { get; set; }
        [ForeignKey("CodGrupoEquip")]
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public int CodEquip { get; set; }
        [ForeignKey("CodEquip")]
        public Equipamento Equipamento { get; set; }
        public decimal Valor { get; set; }
	    public string CodUsuarioCad {get;set;}
	    public string DataHoraCad {get;set;}
	    public string CodUsuarioManut {get;set;}
	    public string DataHoraManut {get;set;}
	    public string CodUsuarioCadastro_DEL {get;set;}
	    public string DataHoraCadastro_DEL {get;set;}
	    public string CodUsuarioManutencao_DEL {get;set;}
	    public string DataHoraManutencao_DEL {get;set;}

    }
}