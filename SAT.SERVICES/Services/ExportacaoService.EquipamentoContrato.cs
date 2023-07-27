using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaEquipamentoContrato(EquipamentoContratoParameters parameters)
        {
            IEnumerable<EquipamentoContrato> equips = (IEnumerable<EquipamentoContrato>)_ecSvc.ObterPorParametros(parameters).Items;

			var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

			if (usuario.CodSetor == 1)
			{
				var equipamentos = equips.Select(eq => 
											new
											{
												ID_Equipamento = eq.CodEquipContrato,
												TipoEquipamento = eq.TipoEquipamento?.NomeTipoEquip ?? Constants.NENHUM_REGISTRO,
												GrupoEquipamento = eq.GrupoEquipamento?.NomeGrupoEquip ?? Constants.NENHUM_REGISTRO,
												Equipamento = eq.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
												Serie = eq.NumSerie ?? Constants.NENHUM_REGISTRO,
												SerieCliente= eq.NumSerieCliente ?? Constants.NENHUM_REGISTRO,
												Cliente= eq.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
												CNPJ_LocalAtendimento = eq.LocalAtendimento?.Cnpj ?? Constants.NENHUM_REGISTRO,
												Contrato = eq.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
												TipoContrato = eq.Contrato?.TipoContrato.NomeTipoContrato ?? Constants.NENHUM_REGISTRO,
												Agencia = eq.LocalAtendimento?.NumAgencia ?? Constants.NENHUM_REGISTRO,
												DC = eq.LocalAtendimento?.DCPosto ?? Constants.NENHUM_REGISTRO,
												NomeDoLocal = eq.LocalAtendimento?.NomeLocal ?? Constants.NENHUM_REGISTRO,
												Endereco = eq.LocalAtendimento?.Endereco ?? Constants.NENHUM_REGISTRO,
												Cidade = eq.LocalAtendimento?.Cidade?.NomeCidade ?? Constants.NENHUM_REGISTRO,
												UF = eq.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF ?? Constants.NENHUM_REGISTRO,
												Filial = eq.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
												Autorizada = eq.Autorizada?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
												Regiao = eq.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO,
												SLA = eq.AcordoNivelServico?.NomeSLA ?? Constants.NENHUM_REGISTRO,
												DistanciaPAT_Res = eq.DistanciaPatRes ?? 0,
												PA = eq.RegiaoAutorizada?.PA ?? 0,
												EquipamentoEmGarantia= eq.IndGarantia == 1 ? "SIM" : "NÃO",	
												Ativo = eq.IndAtivo == 1 ? "SIM" : "NÃO",
												Receita = eq.IndReceita == 1 ? "SIM" : "NÃO",
												Repasse = eq.IndRepasse == 1 ? "SIM" : "NÃO",
												Instalada = eq.IndInstalacao == 1 ? "SIM" : "NÃO",
												DataAtivação = eq.DataAtivacao?.ToString("dd/MM/yy HH:mm"),
												DataDesativação = eq.DataDesativacao?.ToString("dd/MM/yy HH:mm"),
												DataInicGarantia = eq.DataInicGarantia?.ToString("dd/MM/yy HH:mm"),
												DataFimGarantia = eq.DataFimGarantia?.ToString("dd/MM/yy HH:mm"),
												CodigoBMP = eq.CodBMP ?? Constants.NENHUM_REGISTRO,
												Sequencia = eq.Sequencia ?? Constants.NENHUM_REGISTRO,
												Semat = eq.IndSemat == 1 ? "SIM" : "NÃO",
												CNPJFaturamento = eq.LocalAtendimento?.CnpjFaturamento ?? Constants.NENHUM_REGISTRO,
												ReceitaValor = eq.ValorReceita,
												SoftwareEmbarcado = string.Format("{0:C}", eq.ValorSoftwareEmbarcado),
												MonitoramentoRemoto = string.Format("{0:C}", eq.ValorMonitoramentoRemoto),
												RepasseValor = eq.ValorRepasse ?? 0,
												RegraEquivalencia = eq.Equipamento?.Equivalencia?.Regra ?? Constants.NENHUM_REGISTRO,
												ValorEquivalencia = eq.Equipamento?.Equivalencia?.ValorCalculado ?? 0													
											});
			    var wsEq = Workbook.Worksheets.Add("Equipamentos");
            	wsEq.Cell(2, 1).Value = equipamentos;
            	WriteHeaders(equipamentos.FirstOrDefault(), wsEq);
			}

			else
			{
				var equipamentos = equips.Select(eq => 
											new
											{
												ID_Equipamento = eq.CodEquipContrato,
												TipoEquipamento = eq.TipoEquipamento?.NomeTipoEquip ?? Constants.NENHUM_REGISTRO,
												GrupoEquipamento = eq.GrupoEquipamento?.NomeGrupoEquip ?? Constants.NENHUM_REGISTRO,
												Equipamento = eq.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
												Serie = eq.NumSerie ?? Constants.NENHUM_REGISTRO,
												SerieCliente= eq.NumSerieCliente ?? Constants.NENHUM_REGISTRO,
												Cliente= eq.Cliente.NomeFantasia ?? Constants.NENHUM_REGISTRO,
												CNPJ_LocalAtendimento = eq.LocalAtendimento?.Cnpj ?? Constants.NENHUM_REGISTRO,
												Contrato = eq.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
												TipoContrato = eq.Contrato?.TipoContrato.NomeTipoContrato ?? Constants.NENHUM_REGISTRO,
												Agencia = eq.LocalAtendimento?.NumAgencia ?? Constants.NENHUM_REGISTRO,
												DC = eq.LocalAtendimento?.DCPosto ?? Constants.NENHUM_REGISTRO,
												NomeDoLocal = eq.LocalAtendimento?.NomeLocal ?? Constants.NENHUM_REGISTRO,
												Endereco = eq.LocalAtendimento?.Endereco ?? Constants.NENHUM_REGISTRO,
												Cidade = eq.LocalAtendimento?.Cidade?.NomeCidade ?? Constants.NENHUM_REGISTRO,
												UF = eq.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF ?? Constants.NENHUM_REGISTRO,
												Filial = eq.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
												Autorizada = eq.Autorizada?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
												Regiao = eq.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO,
												SLA = eq.AcordoNivelServico?.NomeSLA ?? Constants.NENHUM_REGISTRO,
												DistanciaPAT_Res = eq.DistanciaPatRes ?? 0,
												PA = eq.RegiaoAutorizada?.PA ?? 0,
												EquipamentoEmGarantia= eq.IndGarantia == 1 ? "SIM" : "NÃO",	
												Ativo = eq.IndAtivo == 1 ? "SIM" : "NÃO",
												Receita = eq.IndReceita == 1 ? "SIM" : "NÃO",
												Repasse = eq.IndRepasse == 1 ? "SIM" : "NÃO",
												Instalada = eq.IndInstalacao == 1 ? "SIM" : "NÃO",
												DataAtivação = eq.DataAtivacao?.ToString("dd/MM/yy HH:mm"),
												DataDesativação = eq.DataDesativacao?.ToString("dd/MM/yy HH:mm"),
												DataInicGarantia = eq.DataInicGarantia?.ToString("dd/MM/yy HH:mm"),
												DataFimGarantia = eq.DataFimGarantia?.ToString("dd/MM/yy HH:mm"),
												CodigoBMP = eq.CodBMP ?? Constants.NENHUM_REGISTRO,
												Sequencia = eq.Sequencia ?? Constants.NENHUM_REGISTRO,
												Semat = eq.IndSemat == 1 ? "SIM" : "NÃO",
												CNPJFaturamento = eq.LocalAtendimento?.CnpjFaturamento ?? Constants.NENHUM_REGISTRO											
											});

				var wsEq = Workbook.Worksheets.Add("Equipamentos");
            	wsEq.Cell(2, 1).Value = equipamentos;
            	WriteHeaders(equipamentos.FirstOrDefault(), wsEq);
			}
        }
    }
}
