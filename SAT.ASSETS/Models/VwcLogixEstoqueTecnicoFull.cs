using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueTecnicoFull
    {
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("dat_emis_nf")]
        [StringLength(50)]
        public string DatEmisNf { get; set; }
        [Column("dataEmissaoAntiga")]
        [StringLength(50)]
        public string DataEmissaoAntiga { get; set; }
        [Column("qtd_tot_remessa")]
        [StringLength(50)]
        public string QtdTotRemessa { get; set; }
        [Column("qtd_tot_recebida")]
        [StringLength(50)]
        public string QtdTotRecebida { get; set; }
    }
}
