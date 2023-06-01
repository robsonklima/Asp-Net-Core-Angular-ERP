using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwForeignKey
    {
        [Required]
        [StringLength(128)]
        public string TableName { get; set; }
        [Required]
        [StringLength(128)]
        public string ColumnName { get; set; }
        [Column("FTableName")]
        [StringLength(128)]
        public string FtableName { get; set; }
        [Column("FColumnName")]
        [StringLength(128)]
        public string FcolumnName { get; set; }
    }
}
