using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RATBanrisul_ARQ_MORTO")]
    public partial class RatbanrisulArqMorto
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodRATBanrisul")]
        public int CodRatbanrisul { get; set; }
        [StringLength(20)]
        public string NumSerieInst { get; set; }
        [StringLength(20)]
        public string NumSerieRet { get; set; }
        [StringLength(15)]
        public string Rede { get; set; }
        public int? CodTipoComunicacao { get; set; }
        [StringLength(50)]
        public string NumeroChipInstalado { get; set; }
        public int? CodOperadoraTelefoniaChipInstalado { get; set; }
        [StringLength(50)]
        public string NumeroChipRetirado { get; set; }
        public int? CodOperadoraTelefoniaChipRetirado { get; set; }
        public int? CodMotivoComunicacao { get; set; }
        [StringLength(2000)]
        public string ObsMotivoComunicacao { get; set; }
        public bool? AtendimentoRealizadoPorTelefone { get; set; }
    }
}
