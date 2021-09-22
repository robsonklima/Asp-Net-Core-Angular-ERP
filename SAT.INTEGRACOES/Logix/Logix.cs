using SAT.INTEGRACOES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.INTEGRACOES.Logix
{
    public class Logix : BaseIntegracao, IIntegracao
    {
        public void ExecutaIntegracao()
        {
            throw new NotImplementedException();
        }

        public void RetornoIntegracao()
        {
            throw new NotImplementedException();
        }

        public void TransformaDadosIntegracao()
        {
            throw new NotImplementedException();
        }

        public void VerificaNovaIntegracao()
        {
            throw new NotImplementedException();
        }
    }
}


//using SAT.API.Repositories;
//using SAT.INTEGRACOES.Context;
//using SAT.INTEGRACOES.Repositories.Interfaces;
//using SAT.MODELS.Entities;
//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace SAT.INTEGRACOES.Integracoes.Logix
//{
//    class Logix : IIntegracao
//    {
//        public AppDbContext DbContext { get; set; }
//        public Integracao Integracao { get; set; }
//        public string NomeIntegracao => this.GetType().Name;

//        public Logix(AppDbContext dbContext)
//        {
//            this.DbContext = dbContext;
//        }

//        public void BeforeExecute()
//        {
//            try
//            {
//                //this.Integracao = new Integracao();

//                //this.Integracao.NomeIntegracao = this.NomeIntegracao;
//                //this.Integracao.DataInicioExecucao = DateTime.Now;

//                //this.DbContext.Integracao.Add(this.Integracao);
//                //this.DbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                LoggerRepository.Instance.LogError(
//                    string.Format("Integracao {0} executada em {1} : Error - {2}",
//                    this.NomeIntegracao,
//                    DateTime.Now,
//                    ex.InnerException.Message));
//            }
//        }

//        public class Indicador
//        {
//            public string Nome { get; set; }
//            public decimal Valor { get; set; }
//            public IEnumerable<Indicador> Filho { get; set; }
//        }

//        public void Amem()
//        {
//            DateTime data = new DateTime(2021, 07, 20);
//            var lista2 = this.DbContext.OrdemServico;
//            var lista = this.DbContext.OrdemServico.Where(s => s.DataHoraAberturaOS >= data).ToList();
//            var listaCliente = lista.Select(s => s.Cliente).ToList();
//            // var listaFilial = lista.Select(s => s.Filial).ToList();

//            var indicadores = new string[] { "Chamados" };

//            var u = (from ind in indicadores
//                     select new Indicador()
//                     {
//                         Nome = ind,
//                         Filho = (from cliente in listaCliente
//                                  group cliente by cliente.NomeFantasia into newGrupoCliente
//                                  select new Indicador()
//                                  {
//                                      Nome = newGrupoCliente.Key,
//                                      Valor = newGrupoCliente.Count(),
//                                      Filho = (from filial in lista.Where(s => s.Cliente.NomeFantasia == newGrupoCliente.Key).Select(s => s.Filial).ToList()
//                                               group filial by filial.NomeFilial into newGrupoFilial
//                                               select new Indicador()
//                                               {
//                                                   Nome = newGrupoFilial.Key,
//                                                   Valor = newGrupoFilial.Count(),
//                                                   // Filho =
//                                               }
//                                )
//                                  }
//                                )
//                     }
//             );

//            var cui8sau = u;
//        }

//        public void Execute()
//        {
//            DateTime dataMinima = DateTime.Now.AddDays(-30);

//            try
//            {
//                //var j2 = this.DbContext.Orc
//                //            .Include(s => s.OrdemServico)
//                //            .Include(s => s.OrdemServico.Cliente)
//                //            .Include(s => s.OrcamentosFaturamento)
//                //            .in
//                //.Include(c => c.OrdemServico.RelatoriosAtendimento)


//                //.ThenInclude(q => q.RelatorioAtendimentoDetalhes)


//                //.ThenInclude(t => t.Cliente )


//                var join1 = (from orc in this.DbContext.Orc
//                             join os in this.DbContext.OrdemServico on orc.CodigoOrdemServico equals os.CodOS
//                             join cliente in this.DbContext.Cliente on os.CodCliente equals cliente.CodCliente

//                             join orcFatur in this.DbContext.OrcamentosFaturamento on orc.CodOrc equals orcFatur.CodOrcamento into orcFatTemp
//                             from joinOrcFat in orcFatTemp.DefaultIfEmpty()

//                             join equipContr in this.DbContext.EquipamentoContrato on os.CodEquipContrato equals equipContr.CodEquipContrato into eqContTemp
//                             from joinEquipContr in eqContTemp.DefaultIfEmpty()

//                             join contrato in this.DbContext.Contrato on joinEquipContr.CodContrato equals contrato.CodContrato into contrTemp
//                             from joinContrato in contrTemp.DefaultIfEmpty()

//                             join orcMot in this.DbContext.OrcMotivo on orc.CodigoMotivo equals orcMot.CodOrcMotivo
//                             join localAt in this.DbContext.LocalAtendimento on os.CodPosto equals localAt.CodPosto
//                             join cidade in this.DbContext.Cidade on localAt.CodCidade equals cidade.CodCidade
//                             join uf in this.DbContext.UnidadeFederativa on cidade.CodUF equals uf.CodUF

//                             join orcMat in this.DbContext.OrcMaterial on orc.CodOrc equals orcMat.CodOrc into matTemp
//                             from joinMat in matTemp.DefaultIfEmpty()

//                             join orcDesl in this.DbContext.OrcDeslocamento on orc.CodOrc equals orcDesl.CodOrc into deslTemp
//                             from joinDesl in deslTemp.DefaultIfEmpty()

//                             join orcMaoObra in this.DbContext.OrcMaoObra on orc.CodOrc equals orcMaoObra.CodOrc into maoObraTemp
//                             from joinMaoObra in maoObraTemp.DefaultIfEmpty()

//                             join clPosVenda in this.DbContext.ClientePOSVendas on
//                             Convert.ToString(os.CodCliente) equals clPosVenda.CodCliente into clPosTemp
//                             from joinClPos in clPosTemp.DefaultIfEmpty()

//                             join usuario in this.DbContext.Usuario on joinClPos.CodUsuario equals usuario.CodUsuario into usrTemp
//                             from joinUsr in usrTemp.DefaultIfEmpty()

//                             join endereco in this.DbContext.OrcEndereco.Where(end => end.Tipo == 2) on orc.CodOrc equals endereco.CodOrc into endTemp
//                             from joinEnd in endTemp.DefaultIfEmpty()

//                             where

//                             //Aprovado
//                             os.CodTipoIntervencao == 17 &&
//                             //Igual ou maior que 30 dias da sua emissão.
//                             os.DataHoraFechamento.Value.Date <= dataMinima.Date &&
//                             //Apenas chamados Fechados
//                             os.CodStatusServico == 3 &&
//                             //Nota Fiscal não preenchida.  
//                             joinOrcFat.NumNF == null &&
//                             //Data Emissão da Nota Fiscal não preenchida.  
//                             joinOrcFat.DataEmissaoNF == null &&

//                             !string.IsNullOrWhiteSpace(orc.Numero) &&

//                             (orc.ValorTotal.Value - (joinMat.ValorTotal ?? 0)) > 0

//                             select orc.Numero).ToList();

//                var cu = join1;
//            }
//            catch (Exception ex)
//            {
//                LoggerRepository.Instance.LogError(
//                    string.Format("Integracao {0} executada em {1} : Error - {2}",
//                    this.NomeIntegracao,
//                    DateTime.Now,
//                    ex.InnerException.Message));
//            }
//        }

//        public void AfterExecute()
//        {
//            try
//            {
//                //this.Integracao.DataFimExecucao = DateTime.Now;
//                //this.DbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                LoggerRepository.Instance.LogError(
//                    string.Format("Integracao {0} executada em {1} : Error - {2}",
//                    this.NomeIntegracao,
//                    DateTime.Now,
//                    ex.InnerException.Message));
//            }
//        }
//    }
//}
