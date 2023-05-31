using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDespesasFrotaTecnicosPagamento
    {
        [Required]
        [StringLength(4000)]
        public string Integrado { get; set; }
        [StringLength(150)]
        public string Observacao { get; set; }
        [StringLength(4000)]
        public string SaldoCartao { get; set; }
        [StringLength(31)]
        public string CodDespesaProtocolo { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
        [Required]
        [StringLength(3)]
        public string Ativo { get; set; }
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [StringLength(4000)]
        public string DataInicio { get; set; }
        [StringLength(4000)]
        public string DataFim { get; set; }
        public int? KmPercorrido { get; set; }
        [StringLength(8000)]
        public string PrecoLitro { get; set; }
        [StringLength(8000)]
        public string DespesaOutras { get; set; }
        [StringLength(8000)]
        public string DespesaCombustivel { get; set; }
        [StringLength(8000)]
        public string DespesaTotal { get; set; }
        public byte? IndCredito { get; set; }
        public byte? IndVerificacao { get; set; }
        public byte? IndCompensacao { get; set; }
        [StringLength(4000)]
        public string DespesaProtocoloPeriodoTecnicoDataHoraCad { get; set; }
    }
}
