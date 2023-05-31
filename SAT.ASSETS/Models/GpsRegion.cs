using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GpsRegion")]
    public partial class GpsRegion
    {
        [Key]
        [Column("GpsRegionID")]
        public int GpsRegionId { get; set; }
        public int RegionCode { get; set; }
        [Required]
        [StringLength(20)]
        public string RegionColor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FillDate { get; set; }
    }
}
