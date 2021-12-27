using System;
using System.IO;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository _fotoRepo;

        public FotoService(IFotoRepository fotoRepo)
        {
            _fotoRepo = fotoRepo;
        }

        public Foto Criar(Foto foto)
        {
            _fotoRepo.Criar(foto);
            SalvarFotoServer(foto);
            return foto;
        }

        public void Deletar(int codigo)
        {
            _fotoRepo.Deletar(codigo);
        }

        public Foto ObterPorCodigo(int codigo)
        {
            return _fotoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(FotoParameters parameters)
        {
            var fotos = _fotoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = fotos,
                TotalCount = fotos.TotalCount,
                CurrentPage = fotos.CurrentPage,
                PageSize = fotos.PageSize,
                TotalPages = fotos.TotalPages,
                HasNext = fotos.HasNext,
                HasPrevious = fotos.HasPrevious
            };

            return lista;
        }

        public void SalvarFotoServer(Foto foto)
        {
            if (!string.IsNullOrWhiteSpace(foto.Base64))
            {
                string target = Directory.GetCurrentDirectory() + "/Upload";

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                string imageName = foto.NomeFoto;
                string imgPath = Path.Combine(target, imageName);
                byte[] imageBytes = Convert.FromBase64String(foto.Base64.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", ""));
                File.WriteAllBytes(imgPath, imageBytes);
            }
        }
    }
}
