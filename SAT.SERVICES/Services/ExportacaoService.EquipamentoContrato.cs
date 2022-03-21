using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
	public partial class ExportacaoService
	{
		protected IEnumerable<object> ObterEquipamentoContrato(EquipamentoContratoParameters parameters)
		{
			throw new NotImplementedException();
		}

		protected void GerarPlanilhaEquipamentoContrato(EquipamentoContratoParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}
