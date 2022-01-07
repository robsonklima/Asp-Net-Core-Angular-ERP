using System.Collections.Generic;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IMediaAtendimentoTecnicoRepository
    {
        void AtualizarOuCriar(MediaAtendimentoTecnico media);
        void AtualizaMediaTecnico();
    }
}