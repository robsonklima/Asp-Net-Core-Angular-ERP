using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CheckinCheckoutRelatorio")]
    public partial class CheckinCheckoutRelatorio
    {
        [Key]
        public int CodCheckinCheckoutRelatorio { get; set; }
        [Column("CodOS")]
        [StringLength(10)]
        public string CodOs { get; set; }
        [StringLength(20)]
        public string CheckinDataHoraCadSmartphone { get; set; }
        [StringLength(20)]
        public string DataHoraInicio { get; set; }
        [StringLength(20)]
        public string DataHoraSolucao { get; set; }
        [StringLength(20)]
        public string CheckoutDataHoraCadSmartphone { get; set; }
        [StringLength(10)]
        public string CheckinMenorInicio { get; set; }
        [StringLength(10)]
        public string CheckoutMaiorSolucao { get; set; }
        [StringLength(30)]
        public string CheckinLatitude { get; set; }
        [StringLength(30)]
        public string CheckinLongitude { get; set; }
        [StringLength(30)]
        public string CheckoutLatitude { get; set; }
        [StringLength(20)]
        public string CheckoutLongitude { get; set; }
        [StringLength(150)]
        public string NomeLocalAtendimento { get; set; }
        [StringLength(100)]
        public string NomeTecnico { get; set; }
        [StringLength(30)]
        public string LocalAtendimentoLatitude { get; set; }
        [StringLength(30)]
        public string LocalAtendimentoLongitude { get; set; }
        [StringLength(30)]
        public string DistanciaCheckinLocal { get; set; }
        [StringLength(30)]
        public string DuracaoCheckinLocal { get; set; }
        [StringLength(30)]
        public string DistanciaCheckoutLocal { get; set; }
        [StringLength(30)]
        public string DuracaoCheckoutLocal { get; set; }
    }
}
