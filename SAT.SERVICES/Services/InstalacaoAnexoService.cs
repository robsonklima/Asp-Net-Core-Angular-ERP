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
    public class InstalacaoAnexoService : IInstalacaoAnexoService   
    {
        private readonly IInstalacaoAnexoRepository _instalacaoAnexoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoAnexoService(
            IInstalacaoAnexoRepository instalacaoAnexoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoAnexoRepo = instalacaoAnexoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoAnexo Criar(InstalacaoAnexo instalacaoAnexo)
        {
            string target = Directory.GetCurrentDirectory() + "/Upload/Instalacao";

            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            string fileName = instalacaoAnexo.NomeAnexo;
            string imgPath = Path.Combine(target, fileName);

            string existsFile = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == fileName.Split('.')[0]);

            if (!string.IsNullOrWhiteSpace(existsFile))
                File.Delete(existsFile);

            byte[] imageBytes = Convert.FromBase64String(instalacaoAnexo.Base64);
            File.WriteAllBytes(imgPath, imageBytes);

            instalacaoAnexo.CodInstalAnexo = _sequenciaRepo.ObterContador("InstalAnexo");
            _instalacaoAnexoRepo.Criar(instalacaoAnexo);
            
            return instalacaoAnexo;
        }

        public void Deletar(int codigo)
        {
            _instalacaoAnexoRepo.Deletar(codigo);
        }

        public InstalacaoAnexo ObterPorCodigo(int codigo)
        {
            return _instalacaoAnexoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoAnexoParameters parameters)
        {
            string target = Directory.GetCurrentDirectory() + "\\Upload\\Instalacao\\";
            var instalacaoAnexos = _instalacaoAnexoRepo.ObterPorParametros(parameters);

            for (int i = 0; i < instalacaoAnexos.Count; i++)
            {
                instalacaoAnexos[i].Base64 = StorageBase64.ConverteStorageEmBase64(target + instalacaoAnexos[i].NomeAnexo);
            }

            var lista = new ListViewModel
            {
                Items = instalacaoAnexos,
                TotalCount = instalacaoAnexos.TotalCount,
                CurrentPage = instalacaoAnexos.CurrentPage,
                PageSize = instalacaoAnexos.PageSize,
                TotalPages = instalacaoAnexos.TotalPages,
                HasNext = instalacaoAnexos.HasNext,
                HasPrevious = instalacaoAnexos.HasPrevious
            };

            return lista;
        }
    }
}
