using System;

namespace SAT.MODELS.Entities
{
    public class ImportacaoAdendo
    {
        public int CodAdendo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraProcessamento { get; set; }
        public byte IndAtivo { get; set; }
        public int CodEquipContrato { get; set; }
        public string NomeSla { get; set; }
        public byte IndSemat { get; set; }
        public byte PontoEstrategico { get; set; }
        public byte IndRHorario { get; set; }
        public byte IndRAcesso { get; set; }
        public byte IndPAE { get; set; }
        public string NomeContrato { get; set; }
        public byte IndAtivoEquip { get; set; }
        public decimal DistanciaKmPAT_Res { get; set; }
        public string NumAgenciaDC { get; set; }
        public byte IndInstalacao { get; set; } 
        public int Horas_RAcesso { get; set; }   
        public byte IndReceita { get; set; }
        public byte IndRepasse { get; set; }            
    }
}
