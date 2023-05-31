using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcMtbf1
    {
        public int CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        [Column("IndOS")]
        public int IndOs { get; set; }
        [Column("QtdDiaDiferencaOSAnterior")]
        public int QtdDiaDiferencaOsanterior { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodRegiao { get; set; }
    }
}
