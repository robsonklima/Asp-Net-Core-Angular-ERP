using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SAT.INFRA.Repository
{
    public class MediaAtendimentoTecnicoRepository : IMediaAtendimentoTecnicoRepository
    {
        private readonly AppDbContext _context;

        public MediaAtendimentoTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AtualizarListaAsync(List<MediaAtendimentoTecnico> medias)
        {
            Parallel.Invoke(() =>
            {
                foreach (var media in medias)
                {
                    MediaAtendimentoTecnico m = _context.MediaAtendimentoTecnico
                        .FirstOrDefault(a => a.CodTecnico == media.CodTecnico && a.CodTipoIntervencao == media.CodTipoIntervencao);

                    if (m != null)
                        _context.Entry(m).CurrentValues.SetValues(media);
                    else
                        _context.Add(media);
                }
                _context.SaveChanges();
            });
        }
    }
}