using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepo;

        public TurnoService(ITurnoRepository turnoRepo)
        {
            _turnoRepo = turnoRepo;
        }

        public ListViewModel ObterPorParametros(TurnoParameters parameters)
        {
            var turnos = _turnoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = turnos,
                TotalCount = turnos.TotalCount,
                CurrentPage = turnos.CurrentPage,
                PageSize = turnos.PageSize,
                TotalPages = turnos.TotalPages,
                HasNext = turnos.HasNext,
                HasPrevious = turnos.HasPrevious
            };

            return lista;
        }

        public Turno Criar(Turno turno)
        {
            _turnoRepo.Criar(turno);
            return turno;
        }

        public void Deletar(int codigo)
        {
            _turnoRepo.Deletar(codigo);
        }

        public void Atualizar(Turno turno)
        {
            _turnoRepo.Atualizar(turno);
        }

        public Turno ObterPorCodigo(int codigo)
        {
            return _turnoRepo.ObterPorCodigo(codigo);
        }
    }
}
