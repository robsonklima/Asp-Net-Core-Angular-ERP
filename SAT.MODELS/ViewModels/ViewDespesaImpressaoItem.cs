using System;

namespace SAT.MODELS.ViewModels
{
    public class ViewDespesaImpressaoItem
    {
        public int CodDespesaItem { get; set; }
        public int CodOS { get; set; }
        public int CodRAT { get; set; }
        public string NumRAT { get; set; }
        public int CodDespesaPeriodoTecnico { get; set; }
        public double ValorKM { get; set; }
        public DateTime DataHoraSolucao { get; set; }
        public string DiaSemana { get; set; }
        public string Obs { get; set; }
        public double KmPercorrido { get; set; }
        public string NumNF { get; set; }
        public double DespesaValor { get; set; }
        public int CodDespesaTipo { get; set; }
        public string NomeTipo { get; set; }
        public string EnderecoOrigem { get; set; }
        public string EnderecoDestino { get; set; }
        public string NumBanco { get; set; }
        public string LocalOrigem { get; set; }
        public string LocalDestino { get; set; }
        public DateTime HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public int CodDespesaItemAlerta { get; set; }
        public int CodTecnico { get; set; }
        public int CodDespesaPeriodo { get; set; }
    }
}