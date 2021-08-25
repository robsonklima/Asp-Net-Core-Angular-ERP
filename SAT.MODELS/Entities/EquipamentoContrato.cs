﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class EquipamentoContrato
    {
        [Key]
        public int CodEquipContrato { get; set; }
        public int CodContrato { get; set; }
        [ForeignKey("CodContrato")]
        public Contrato Contrato { get; set; }
        public int CodTipoEquip { get; set; }
        [ForeignKey("CodTipoEquip")]
        public TipoEquipamento TipoEquipamento { get; set; }
        public int CodGrupoEquip { get; set; }
        [ForeignKey("CodGrupoEquip")]
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public int CodEquip { get; set; }
        [ForeignKey("CodEquip")]
        public Equipamento Equipamento { get; set; }
        public int CodSLA { get; set; }
        [ForeignKey("CodSLA")]
        public AcordoNivelServico AcordoNivelServico { get; set; }
        public string NumSerie { get; set; }
        public string NumSerieCliente { get; set; }
        public int CodCliente { get; set; }
        [ForeignKey("CodCliente")]
        public Cliente Cliente { get; set; }
        public int CodPosto { get; set; }
        [ForeignKey("CodPosto")]
        public LocalAtendimento LocalAtendimento { get; set; }
        public int CodRegiao { get; set; }
        [ForeignKey("CodRegiao")]
        public Regiao Regiao { get; set; }
        public int CodAutorizada { get; set; }
        [ForeignKey("CodAutorizada")]
        public Autorizada Autorizada { get; set; }
        public int CodFilial { get; set; }
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }
        [Column("DistanciaPAT_Res")]
        public decimal? DistanciaPatRes { get; set; }
        public byte? IndGarantia { get; set; }
        public DateTime? DataInicGarantia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        public byte IndReceita { get; set; }
        public decimal ValorReceita { get; set; }
        public byte IndRepasse { get; set; }
        public byte IndRepasseIndividual { get; set; }
        public decimal? ValorRepasse { get; set; }
        public decimal ValorDespesa { get; set; }
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte IndInstalacao { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public DateTime? DataDesativacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string Origem { get; set; }
        public DateTime? HoraDispInicio { get; set; }
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        public string CodEquipCliente { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public byte? IndSemat { get; set; }
        public string PontoEstrategico { get; set; }
        public byte? IndRHorario { get; set; }
        public byte? IndRAcesso { get; set; }
        public int? CodTipoLocalAtendimento { get; set; }
        public byte? IndVerao { get; set; }
        public byte? IndPAE { get; set; }
        public byte? IndRetrofit { get; set; }
        public DateTime? DataRetrofit1 { get; set; }
        public DateTime? DataRetrofit2 { get; set; }
        public byte? IndRetrofit2 { get; set; }
        public byte? IndRetrofit3 { get; set; }
        public DateTime? DataRetrofit3 { get; set; }
        public byte? IndMTBF4 { get; set; }
        public string NumEtiquetaEquip { get; set; }
        public DateTime? DataRelatorio { get; set; }
        public byte? IndRelatorio { get; set; }
        public string CodBMP { get; set; }
        public string Sequencia { get; set; }
        public byte? IndMecanismo { get; set; }
        public int? CodDispBBCriticidade { get; set; }
        [ForeignKey("CodFilial, CodRegiao, CodAutorizada")]
        public RegiaoAutorizada RegiaoAutorizada { get; set; }
    }
}
