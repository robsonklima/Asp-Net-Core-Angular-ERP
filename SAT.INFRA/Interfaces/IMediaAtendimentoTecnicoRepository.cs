using System.Collections.Generic;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IMediaAtendimentoTecnicoRepository
    {
        void AtualizarListaAsync(List<MediaAtendimentoTecnico> medias);
    }
}