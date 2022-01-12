using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoDeslocamentoRepository
    {
        void Criar(OrcamentoDeslocamento regiao);
        void Atualizar(OrcamentoDeslocamento deslocamento);
    }
}
