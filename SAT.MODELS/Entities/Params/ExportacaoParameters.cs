using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities.Params
{
    public class ExportacaoParameters : QueryStringParameters
    {
        public string OrdemServicoParameters { get; set; }
		public EquipamentoContratoParameters EquipamentoContratoParameters{ get; set; }
		public int ExportacaoFormato { get; set; }
		public int ExportacaoTipo { get; set; }
    }
}