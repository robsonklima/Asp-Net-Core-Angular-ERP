using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("BaseEquipamentoBanrisul")]
    public partial class BaseEquipamentoBanrisul
    {
        public BaseEquipamentoBanrisul()
        {
            BaseEquipamentoBanrisulItems = new HashSet<BaseEquipamentoBanrisulItem>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInformacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.BaseEquipamentoBanrisuls))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(BaseEquipamentoBanrisulItem.IdBaseEquipamentoBanrisulNavigation))]
        public virtual ICollection<BaseEquipamentoBanrisulItem> BaseEquipamentoBanrisulItems { get; set; }
    }
}
