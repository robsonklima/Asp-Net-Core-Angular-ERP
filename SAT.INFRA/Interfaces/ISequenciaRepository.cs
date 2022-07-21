namespace SAT.INFRA.Interfaces
{
    public interface ISequenciaRepository
    {
        int ObterContador(string tabela);
        int AtualizaContadorOS(int total);
    }
}
