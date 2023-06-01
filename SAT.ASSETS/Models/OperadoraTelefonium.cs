using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class OperadoraTelefonium
    {
        public OperadoraTelefonium()
        {
            ChamadoDadosAdicionaiCodOperadoraTelefoniaHistNavigations = new HashSet<ChamadoDadosAdicionai>();
            ChamadoDadosAdicionaiCodOperadoraTelefoniaNavigations = new HashSet<ChamadoDadosAdicionai>();
            Chamados = new HashSet<Chamado>();
            FecharOspositens = new HashSet<FecharOspositen>();
            Os = new HashSet<O>();
            OsArqMortos = new HashSet<OsArqMorto>();
            RatbanrisulCodOperadoraTelefoniaChipInstaladoNavigations = new HashSet<Ratbanrisul>();
            RatbanrisulCodOperadoraTelefoniaChipRetiradoNavigations = new HashSet<Ratbanrisul>();
        }

        [Key]
        public int CodOperadoraTelefonia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeOperadoraTelefonia { get; set; }
        public bool IndAtivo { get; set; }

        [InverseProperty(nameof(ChamadoDadosAdicionai.CodOperadoraTelefoniaHistNavigation))]
        public virtual ICollection<ChamadoDadosAdicionai> ChamadoDadosAdicionaiCodOperadoraTelefoniaHistNavigations { get; set; }
        [InverseProperty(nameof(ChamadoDadosAdicionai.CodOperadoraTelefoniaNavigation))]
        public virtual ICollection<ChamadoDadosAdicionai> ChamadoDadosAdicionaiCodOperadoraTelefoniaNavigations { get; set; }
        [InverseProperty(nameof(Chamado.CodOperadoraTelefoniaNavigation))]
        public virtual ICollection<Chamado> Chamados { get; set; }
        [InverseProperty(nameof(FecharOspositen.CodOperadoraTelefoniaNavigation))]
        public virtual ICollection<FecharOspositen> FecharOspositens { get; set; }
        [InverseProperty(nameof(O.CodOperadoraTelefoniaNavigation))]
        public virtual ICollection<O> Os { get; set; }
        [InverseProperty(nameof(OsArqMorto.CodOperadoraTelefoniaNavigation))]
        public virtual ICollection<OsArqMorto> OsArqMortos { get; set; }
        [InverseProperty(nameof(Ratbanrisul.CodOperadoraTelefoniaChipInstaladoNavigation))]
        public virtual ICollection<Ratbanrisul> RatbanrisulCodOperadoraTelefoniaChipInstaladoNavigations { get; set; }
        [InverseProperty(nameof(Ratbanrisul.CodOperadoraTelefoniaChipRetiradoNavigation))]
        public virtual ICollection<Ratbanrisul> RatbanrisulCodOperadoraTelefoniaChipRetiradoNavigations { get; set; }
    }
}
