using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SSIS Configurations")]
    public partial class SsisConfiguration
    {
        [Required]
        [StringLength(255)]
        public string ConfigurationFilter { get; set; }
        [StringLength(255)]
        public string ConfiguredValue { get; set; }
        [Required]
        [StringLength(255)]
        public string PackagePath { get; set; }
        [Required]
        [StringLength(20)]
        public string ConfiguredValueType { get; set; }
    }
}
