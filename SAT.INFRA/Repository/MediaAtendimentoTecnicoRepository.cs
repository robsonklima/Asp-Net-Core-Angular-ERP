using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class MediaAtendimentoTecnicoRepository : IMediaAtendimentoTecnicoRepository
    {
        private readonly AppDbContext _context;

        public MediaAtendimentoTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AtualizarOuCriar(MediaAtendimentoTecnico media)
        {
            MediaAtendimentoTecnico m = _context.MediaAtendimentoTecnico
                .FirstOrDefault(a => a.CodTecnico == media.CodTecnico && a.CodTipoIntervencao == media.CodTipoIntervencao);

            if (m != null)
            {
                _context.ChangeTracker.Clear();
                media.CodMediaAtendimentoTecnico = m.CodMediaAtendimentoTecnico;
                _context.Entry(m).CurrentValues.SetValues(media);
            }
            else
                _context.Add(media);

            _context.SaveChanges();
        }
    }
}
