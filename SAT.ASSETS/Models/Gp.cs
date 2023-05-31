using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Gp
    {
        [Key]
        [Column("GpsID")]
        public int GpsId { get; set; }
        public int CodSimCard { get; set; }
        [Required]
        [StringLength(1)]
        public string Status { get; set; }
        public double Latitude { get; set; }
        [Required]
        [StringLength(1)]
        public string LatitudePosition { get; set; }
        public double Longitude { get; set; }
        [Required]
        [StringLength(1)]
        public string LongitudePosition { get; set; }
        public double Speed { get; set; }
        public double Heading { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UtcDatetime { get; set; }
        [Required]
        [StringLength(10)]
        public string MagneticVariation { get; set; }
        [Required]
        [StringLength(1)]
        public string Declination { get; set; }
        [Required]
        [StringLength(1)]
        public string IndicatorMode { get; set; }
        [Required]
        [StringLength(14)]
        public string Checksum { get; set; }
        [Required]
        [StringLength(4)]
        public string Command { get; set; }
        [Required]
        [StringLength(10)]
        public string Hdop { get; set; }
        public double MileMeter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FillDate { get; set; }

        [ForeignKey(nameof(CodSimCard))]
        [InverseProperty(nameof(SimCard.Gps))]
        public virtual SimCard CodSimCardNavigation { get; set; }
    }
}
