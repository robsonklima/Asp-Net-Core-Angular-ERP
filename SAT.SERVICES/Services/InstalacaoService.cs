using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoService : IInstalacaoService
    {
        private readonly IInstalacaoRepository _instalacaoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IContratoEquipamentoRepository _contratoEquipRepo;

        public InstalacaoService(
            IInstalacaoRepository instalacaoRepo,
            ISequenciaRepository sequenciaRepo,
            IContratoEquipamentoRepository ContratoEquipRepo
        )
        {
            _instalacaoRepo = instalacaoRepo;
            _sequenciaRepo = sequenciaRepo;
            _contratoEquipRepo = ContratoEquipRepo;
        }

        public Instalacao ObterPorCodigo(int codigo)
        {
            Instalacao instalacao = _instalacaoRepo.ObterPorCodigo(codigo);

            if (instalacao != null) {
                if (instalacao.Contrato != null) {
                    var contratosEquipamento = _contratoEquipRepo.ObterPorParametros(new ContratoEquipamentoParameters() {
                        CodContrato = instalacao.Contrato.CodContrato,
                        CodEquip = instalacao.CodEquip,
                        CodGrupoEquip = instalacao.CodGrupoEquip,
                        CodTipoEquip = instalacao.CodTipoEquip
                    });

                    if (contratosEquipamento.Count > 0) {
                        instalacao.Contrato.ContratoEquipamento = contratosEquipamento[0];
                    }
                }
            }

            return instalacao;
        }

        public ListViewModel ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalacoes,
                TotalCount = instalacoes.TotalCount,
                CurrentPage = instalacoes.CurrentPage,
                PageSize = instalacoes.PageSize,
                TotalPages = instalacoes.TotalPages,
                HasNext = instalacoes.HasNext,
                HasPrevious = instalacoes.HasPrevious
            };
        }

        public Instalacao Criar(Instalacao instalacao)
        {
            instalacao.CodInstalacao = _sequenciaRepo.ObterContador("Instalacao");
            _instalacaoRepo.Criar(instalacao);
            return instalacao;
        }

        public void Deletar(int codigo)
        {
            _instalacaoRepo.Deletar(codigo);
        }

        public void Atualizar(Instalacao instalacao)
        {
            _instalacaoRepo.Atualizar(instalacao);
        }
    }
}
