using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("ContratoEquipData")]
    public class ContratoEquipamentoData
    {
        [Key]
        public int CodContratoEquipData { get; set; }
        public string NomeData { get; set; }
        public string DescData { get; set; }
        public byte IndEntrega { get; set; }
        public byte IndInstalacao { get; set; }
        public byte IndGarantia { get; set; }
        public byte? IndAtivo { get; set; }
        
    }
}
