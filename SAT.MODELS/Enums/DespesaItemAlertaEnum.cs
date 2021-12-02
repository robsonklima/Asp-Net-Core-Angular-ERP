namespace SAT.MODELS.Enums
{
    public enum DespesaItemAlertaEnum
    {
        Indefinido = 0,
        TecnicoTeveRefeicaoEmHorarioNaoExtraEmDiaSemana = 1,
        TecnicoTeveMaisDeDuasRefeicoesEmFinalSemana = 2,
        TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado = 3,
        TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista = 4,
        SistemaIndisponivelTecnicoTeveQuilometragemNaoValidada = 5,
        SistemaNaoEncontrouCoordenadaOrigem = 6,
        SistemaNaoEncontrouCoordenadaDestino = 7,
        SistemaNaoEncontrouCoordenadaOrigemNemCoordenadaDestino = 8,
        SistemaNaoEncontrouRota = 9,
        SistemaCalculouRotaCentroDasCidades = 10,
        SabadoDomingoDespesaDeAlmocoDeveSerFeitaAposCatorzeHorasJantaAposVinteHoras = 11,
        TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro = 12
    }
}