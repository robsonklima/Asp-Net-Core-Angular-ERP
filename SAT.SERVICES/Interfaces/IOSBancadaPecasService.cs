using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IOSBancadaPecasService
    {
        ListViewModel ObterPorParametros(OSBancadaPecasParameters parameters);
        OSBancadaPecas Criar(OSBancadaPecas osBancadaPecas);
        void Deletar(int codigo, int codigoPeca);
        void Atualizar(OSBancadaPecas osBancadaPecas);
    }
}
