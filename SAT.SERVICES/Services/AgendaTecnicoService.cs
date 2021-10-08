using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;

namespace SAT.SERVICES.Services
{
    public class AgendaTecnicoService : IAgendaTecnicoService
    {
        private IAgendaTecnicoRepository _agendaRepo;
        private readonly ITecnicoRepository _tecnicoRepo;

        public AgendaTecnicoService(
            IAgendaTecnicoRepository agendaRepo,
            ITecnicoRepository tecnicoRepo)
        {
            _agendaRepo = agendaRepo;
            _tecnicoRepo = tecnicoRepo;
        }


        public List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            return _agendaRepo.ObterPorParametros(parameters);
        }

        private string ObterCor()
        {
            string[] cores = {
                "bg-black", "bg-gray-300", "bg-gray-400", "bg-gray-500", "bg-gray-600", "bg-gray-700",
                "bg-gray-800", "bg-gray-900", "bg-red-300", "bg-red-400", "bg-red-500", "bg-red-600",
                "bg-red-700", "bg-red-800", "bg-red-900", "bg-yellow-300", "bg-yellow-400",
                "bg-yellow-500", "bg-yellow-600", "bg-yellow-700", "bg-yellow-800", "bg-yellow-900",
                "bg-green-300", "bg-green-400", "bg-green-500", "bg-green-600", "bg-green-700",
                "bg-green-800", "bg-green-900", "bg-blue-300", "bg-blue-400", "bg-blue-500", "bg-blue-600",
                "bg-blue-700", "bg-blue-800", "bg-blue-900", "bg-indigo-300", "bg-indigo-400", "bg-indigo-500",
                "bg-indigo-600", "bg-indigo-700", "bg-indigo-800", "bg-indigo-900", "bg-purple-300",
                "bg-purple-400", "bg-purple-500", "bg-purple-600", "bg-purple-700", "bg-purple-800",
                "bg-purple-900", "bg-pink-300", "bg-pink-400", "bg-pink-500", "bg-pink-600",
                "bg-pink-700", "bg-pink-800", "bg-pink-900"
            };

            return cores[new Random().Next(0, cores.Length)];
        }

        public void Atualizar(AgendaTecnico agenda)
        {
            _agendaRepo.Atualizar(agenda);
        }

        public void Deletar(int codigo)
        {
            _agendaRepo.Deletar(codigo);
        }

        public void Criar(AgendaTecnico agenda)
        {
            _agendaRepo.Criar(agenda);
        }
    }
}
