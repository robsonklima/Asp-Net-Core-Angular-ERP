using System;
using System.IO;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public class TicketAnexoService : ITicketAnexoService
    {
        private readonly ITicketAnexoRepository _ticketAnexoRepo;

        public TicketAnexoService(
            ITicketAnexoRepository ticketAnexoRepo
        )
        {
            _ticketAnexoRepo = ticketAnexoRepo;
        }

        public TicketAnexo ObterPorCodigo(int codigo)
        {
            return _ticketAnexoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketAnexoParameters parameters)
        {
            var tickets = _ticketAnexoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tickets,
                TotalCount = tickets.TotalCount,
                CurrentPage = tickets.CurrentPage,
                PageSize = tickets.PageSize,
                TotalPages = tickets.TotalPages,
                HasNext = tickets.HasNext,
                HasPrevious = tickets.HasPrevious
            };

            return lista;
        }

        public TicketAnexo Criar(TicketAnexo anexo)
        {
            if (!string.IsNullOrWhiteSpace(anexo.Base64))
            {
                string target = Directory.GetCurrentDirectory() + "/Upload";

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                var extension = FilesHelper.CheckExtension(anexo.Base64.Substring(0, 5));
                string fileName = DateTime.Now.ToString("yyyy-MM-dd_hh_mm_ss_fff") + "." + extension;
                string imgPath = Path.Combine(target, fileName);
                string existsFile = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == fileName.Split('.')[0]);
                if (!string.IsNullOrWhiteSpace(existsFile))
                {
                    File.Delete(existsFile);
                }

                anexo.Nome = fileName;

                byte[] imageBytes = Convert.FromBase64String(anexo.Base64);
                File.WriteAllBytes(imgPath, imageBytes);
            }

            return _ticketAnexoRepo.Criar(anexo);
        }

        public TicketAnexo Deletar(int codigo)
        {
            var anexo = _ticketAnexoRepo.ObterPorCodigo(codigo);
            string target = Directory.GetCurrentDirectory() + "/Upload";

            string existsFile = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == anexo.Nome.Split('.')[0]);
            
            if (!string.IsNullOrWhiteSpace(existsFile))
            {
                File.Delete(existsFile);
            }

            return _ticketAnexoRepo.Deletar(codigo);
        }

        public TicketAnexo Atualizar(TicketAnexo anexo)
        {
            return _ticketAnexoRepo.Atualizar(anexo);
        }
    }
}
