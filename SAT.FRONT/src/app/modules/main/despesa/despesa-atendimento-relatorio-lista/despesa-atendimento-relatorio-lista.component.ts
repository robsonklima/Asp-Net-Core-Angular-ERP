import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaService } from 'app/core/services/despesa.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { DespesaPeriodo } from 'app/core/types/despesa-periodo.types';
import { DespesaData, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimentoData, RelatorioAtendimentoParameters } from 'app/core/types/relatorio-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
	selector: 'app-despesa-atendimento-relatorio-lista',
	templateUrl: './despesa-atendimento-relatorio-lista.component.html',
	styles: [`
        .list-grid-despesa-atendimento-relatorio {
            grid-template-columns: 60px 72px 72px 70px auto 75px 80px 75px 80px 100px 100px;
            @screen sm { grid-template-columns: 60px 72px 72px 70px auto 75px 80px 75px 80px 100px 100px; }
            @screen md { grid-template-columns: 60px 72px 72px 70px auto 75px 80px 75px 80px 100px 100px; }
            @screen lg { grid-template-columns: 60px 72px 72px 70px auto 75px 80px 75px 80px 100px 100px; }
        }
    `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations,
	providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoRelatorioListaComponent extends Filterable implements OnInit, IFilterable {
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild('sidenav') sidenav: MatSidenav;

	isLoading: boolean = false;
	periodo: DespesaPeriodo;
	despesas: DespesaData;
	rats: RelatorioAtendimentoData;
	ordemServico: OrdemServicoData;
	codTecnico: string;

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _despesaPeriodoSvc: DespesaPeriodoService,
		private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
		private _despesaSvc: DespesaService,
		private _ordemServicoSvc: OrdemServicoService) {
		super(_userService, "despesa-atendimento-relatorio");
		this.codTecnico = this._route.snapshot.paramMap.get('codTecnico') || this.userSession.usuario?.codTecnico;
	}

	async ngOnInit() {
		await this.obterDados();

		if (this.sort && this.paginator)
		{
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged()
				this.obterDados();
			});
		}

		this.registerEmitters();
		this._cdr.detectChanges();
	}

	private async obterPeriodo() {
		var codDespesaPeriodo =
			+this._route.snapshot.paramMap.get('codDespesaPeriodo');

		this.periodo =
			(await this._despesaPeriodoSvc.obterPorCodigo(codDespesaPeriodo).toPromise());
	}

	private async obterRATs() {
		if (!this.codTecnico) 
			return;

		const params: RelatorioAtendimentoParameters = {
			codTecnicos: this.codTecnico,
			dataInicio: moment(this.periodo.dataInicio).format('yyyy-MM-DD HH:mm:ss'),
			dataSolucao: moment(this.periodo.dataFim).format('yyyy-MM-DD HH:mm:ss')
		};

		this.rats = await this._relatorioAtendimentoSvc.obterPorParametros(params).toPromise();
	}

	private async obterDespesas() {
		var codigos: string =
			Enumerable.from(this.rats.items)
				.select(i => i.codRAT)
				.distinct()
				.toJoinedString(',');

		this.despesas = this.codTecnico != null && codigos?.length > 0 ?
			(await this._despesaSvc.obterPorParametros
				({
					codRATs: codigos,
					codTecnico: this.codTecnico
				}).toPromise()) : null;
	}

	private async obterOrdensDeServico() {
		var codigos: string =
			Enumerable.from(this.rats.items)
				.select(i => i.codOS)
				.distinct()
				.toJoinedString(',');

		this.ordemServico = (await this._ordemServicoSvc.obterPorParametros
			({ codOS: codigos, pageSize: codigos?.length > 0 ? codigos?.length : 1 }).toPromise());
	}

	public async obterDados() {
		this.isLoading = true;

		await this.obterPeriodo();
		await this.obterRATs();
		await this.obterOrdensDeServico();
		await this.obterDespesas();

		this.isLoading = false;
	}

	registerEmitters(): void {
		this.sidenav?.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	public paginar() {
		this.onPaginationChanged();
		this.obterDados();
	}

	public obterOSCliente(codOS?: number) {
		if (!codOS) return null;
		return Enumerable.from(this.ordemServico?.items)
			.firstOrDefault(i => i.codOS == codOS)?.numOSCliente;
	}

	public obterNomeCliente(codOS?: number) {
		if (!codOS) return null;
		return Enumerable.from(this.ordemServico?.items)
			.firstOrDefault(i => i.codOS == codOS)?.cliente?.nomeFantasia;
	}

	public obterLocalAtendimento(codOS?: number) {
		if (!codOS) return null;
		return Enumerable.from(this.ordemServico?.items)
			.firstOrDefault(i => i.codOS == codOS)?.localAtendimento?.nomeLocal;
	}

	public obterTotalDespesa(codRAT?: number) {
		if (!codRAT) return null;

		return Enumerable.from(this.despesas.items)
			.where(d => d.codRAT == codRAT)
			.sum(d => Enumerable.from(d.despesaItens)
				.where(i => i.indAtivo == statusConst.ATIVO)
				.sum(di => di.despesaValor));
	}

	public obterTotalKm(codRAT?: number) {

		return Enumerable.from(this.despesas.items)
			.where(d => d.codRAT == codRAT)
			.sum(d => Enumerable.from(d.despesaItens)
				.where(di => di.codDespesaTipo == DespesaTipoEnum.KM)
				.sum(di => di.kmPercorrido));
	}
}