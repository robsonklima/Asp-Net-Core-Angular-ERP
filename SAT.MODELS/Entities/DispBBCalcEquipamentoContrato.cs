using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
public class DispBBCalcEquipamentoContrato
{
	public int? CodEquipContrato {get;set;}
	public string NumSerie {get;set;}
	public int? CodEquip {get;set;}
	public string Modelo {get;set;}
	public int? Criticidade {get;set;}
	public int? CodDispBBRegiao {get;set;}
	public string Regiao {get;set;}
	public int? IndispMin {get;set;}
	public int? MinTotais {get;set;}
	public decimal? DispPerc {get;set;}
	public decimal? IndispPerc {get;set;}
	public double? Valor {get;set;}
	public double? IndTaa {get;set;}
	public string AnoMes {get;set;}
	public DateTime? DataHoraCad {get;set;}
}
}
