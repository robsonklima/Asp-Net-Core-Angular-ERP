using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaTipoService
    {
        ListViewModel ObterPorParametros(DespesaTipoParameters parameters);
        DespesaTipo Criar(DespesaTipo despesaTipo);
        void Deletar(int codigo);
        void Atualizar(DespesaTipo despesaTipo);
        DespesaTipo ObterPorCodigo(int codigo);
    }
}