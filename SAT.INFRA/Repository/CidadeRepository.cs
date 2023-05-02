﻿using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using SAT.UTILS;

namespace SAT.INFRA.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly AppDbContext _context;

        public CidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Cidade cidade)
        {
            _context.ChangeTracker.Clear();
            Cidade c = _context.Cidade.FirstOrDefault(c => c.CodCidade == cidade.CodCidade);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(cidade);
                _context.SaveChanges();
            }
        }

        public void Criar(Cidade cidade)
        {
            _context.Add(cidade);
            _context.SaveChanges();
        }

        public void Deletar(int codCidade)
        {
            Cidade c = _context.Cidade.FirstOrDefault(c => c.CodCidade == codCidade);

            if (c != null)
            {
                _context.Cidade.Remove(c);
                _context.SaveChanges();
            }
        }

        public Cidade BuscaCidadePorNome(string nomeCidade)
        {
            Cidade[] listaCidades = _context.Cidade
                .Include(f => f.UnidadeFederativa)
                .ThenInclude(f => f.Pais)
                .Where(c => c.NomeCidade.StartsWith(nomeCidade.Substring(0, 1))).ToArray();

            Cidade retorno = null;
            foreach (Cidade cidade in listaCidades)
            {
                string regex = Regex.Replace(cidade.NomeCidade, "[^a-zA-Z]+", "").ToLower();

                if (regex == nomeCidade)
                {
                    retorno = cidade;
                    break;
                }
            }

            return retorno;
        }

        public Cidade ObterPorCodigo(int codigo)
        {
            return _context.Cidade
                .Include(c => c.UnidadeFederativa)
                .FirstOrDefault(c => c.CodCidade == codigo);
        }

        public PagedList<Cidade> ObterPorParametros(CidadeParameters parameters)
        {
            var cidades = _context.Cidade
                .Include(c => c.UnidadeFederativa)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                cidades = cidades.Where(
                    c =>
                    c.CodCidade.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.NomeCidade.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.UnidadeFederativa.SiglaUF.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.UnidadeFederativa.NomeUF.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (!string.IsNullOrWhiteSpace(parameters.NomeCidade))
            {
                cidades.Where(c => c.NomeCidade.Contains(parameters.NomeCidade));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SiglaUF))
            {
                cidades.Where(c => c.UnidadeFederativa.SiglaUF.Contains(parameters.SiglaUF));
            }
            
            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                cidades = cidades.Where(dc => cods.Contains(dc.Filial.CodFilial));
            };
            
            if (!string.IsNullOrWhiteSpace(parameters.CodUFs))
            {
                int[] cods = parameters.CodUFs.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                cidades = cidades.Where(dc => cods.Contains(dc.UnidadeFederativa.CodUF));
            };

            if (parameters.CodCidade.HasValue)
                cidades = cidades.Where(c => c.CodCidade == parameters.CodCidade);

            if (parameters.IndAtivo.HasValue)
                cidades = cidades.Where(c => c.IndAtivo == parameters.IndAtivo);

            if (parameters.CodUF.HasValue)
                cidades = cidades.Where(c => c.CodUF == parameters.CodUF);

            if (parameters.SortActive != null && parameters.SortDirection != null)
                 cidades = cidades.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Cidade>.ToPagedList(cidades, parameters.PageNumber, parameters.PageSize);
        }
    }
}
