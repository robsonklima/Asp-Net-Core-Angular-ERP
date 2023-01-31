using System;
using System.IO;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoAnexoService : IInstalacaoAnexoService
    {
        private readonly IInstalacaoAnexoRepository _instalacaoAnexoRepo;

        public InstalacaoAnexoService(IInstalacaoAnexoRepository instalacaoAnexoRepo)
        {
            _instalacaoAnexoRepo = instalacaoAnexoRepo;
        }

        public InstalacaoAnexo Criar(InstalacaoAnexo instalacaoAnexo)
        {
            _instalacaoAnexoRepo.Criar(instalacaoAnexo);
            SalvarInstalacaoAnexoServer(instalacaoAnexo);
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
            var instalacaoAnexos = _instalacaoAnexoRepo.ObterPorParametros(parameters);

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

        public void SalvarInstalacaoAnexoServer(InstalacaoAnexo instalacaoAnexo)
        {
            if (!string.IsNullOrWhiteSpace(instalacaoAnexo.Base64))
            {
                string target = Directory.GetCurrentDirectory() + "/Upload";

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                string imageName = instalacaoAnexo.NomeAnexo;
                string imgPath = Path.Combine(target, imageName);

                string existsFile = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == imageName.Split('.')[0]);

                if (!string.IsNullOrWhiteSpace(existsFile))
                {
                    File.Delete(existsFile);
                }

                byte[] imageBytes = Convert.FromBase64String(instalacaoAnexo.Base64.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", ""));
                File.WriteAllBytes(imgPath, imageBytes);
            }
        }

        public ImagemPerfilModel BuscarInstalacaoAnexoUsuario(string codUsuario)
        {
            string target = Directory.GetCurrentDirectory() + "/Upload";

			if (!new DirectoryInfo(target).Exists)
			{
				Directory.CreateDirectory(target);
			}

            string imgPath = Directory.GetFiles(target).FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == codUsuario);

            string base64 = string.Empty;
            string extension = string.Empty;

            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                extension = Path.GetExtension(imgPath);
                byte[] bytes = File.ReadAllBytes(imgPath);

                if (bytes.Length > 0)
                {
                    base64 = Convert.ToBase64String(bytes);
                }
            }

            return new ImagemPerfilModel()
            {
                Base64 = base64,
                CodUsuario = codUsuario,
                Mime = Path.GetExtension(extension)
            };
        }

        public void AlterarInstalacaoAnexoPerfil(ImagemPerfilModel model)
        {
            this.SalvarInstalacaoAnexoServer(new InstalacaoAnexo()
            {
                Base64 = model.Base64,
                NomeAnexo = $"{model.CodUsuario}.{model.Mime}"
            });
        }
    }
}
