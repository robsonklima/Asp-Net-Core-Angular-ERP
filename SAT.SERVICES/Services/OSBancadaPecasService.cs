﻿using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OSBancadaPecasService : IOSBancadaPecasService
    {
        private readonly IOSBancadaPecasRepository _osBancadaPecasRepo;

        public OSBancadaPecasService(IOSBancadaPecasRepository osBancadaPecasRepo, ISequenciaRepository sequenciaRepo)
        {
            _osBancadaPecasRepo = osBancadaPecasRepo;
        }

        public void Atualizar(OSBancadaPecas osBancadaPecas)
        {
            this._osBancadaPecasRepo.Atualizar(osBancadaPecas);
        }

        public OSBancadaPecas Criar(OSBancadaPecas osBancadaPecas)
        {
            this._osBancadaPecasRepo.Criar(osBancadaPecas);
            return osBancadaPecas;
        }

        public OSBancadaPecas ObterPorCodigo(int codOsbancada, int codPecaRe5114)
        {
            return _osBancadaPecasRepo.ObterPorCodigo(codOsbancada, codPecaRe5114);
        }

        public void Deletar(int codigo, int codigoPeca)
        {
            this._osBancadaPecasRepo.Deletar(codigo,codigoPeca);
        }
        public ListViewModel ObterPorParametros(OSBancadaPecasParameters parameters)
        {
            var osBancadaPecass = _osBancadaPecasRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = osBancadaPecass,
                TotalCount = osBancadaPecass.TotalCount,
                CurrentPage = osBancadaPecass.CurrentPage,
                PageSize = osBancadaPecass.PageSize,
                TotalPages = osBancadaPecass.TotalPages,
                HasNext = osBancadaPecass.HasNext,
                HasPrevious = osBancadaPecass.HasPrevious
            };

            return lista;
        }
    }
}
