using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GpsParameter")]
    public partial class GpsParameter
    {
        [Key]
        [Column("GpsParameterID")]
        public int GpsParameterId { get; set; }
        [Key]
        public int CodSimCard { get; set; }
        [StringLength(30)]
        public string Apn { get; set; }
        [StringLength(30)]
        public string ApnUsername { get; set; }
        [StringLength(30)]
        public string ApnPassword { get; set; }
        public int? TmMove { get; set; }
        public int? TmPark { get; set; }
        [StringLength(3)]
        public string Protocol { get; set; }
        [Column("IP")]
        [StringLength(15)]
        public string Ip { get; set; }
        public int? Port { get; set; }
        [StringLength(10)]
        public string Password { get; set; }
    }
}
