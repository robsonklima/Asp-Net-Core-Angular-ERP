import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { StringExtensions } from 'app/core/extensions/string.extensions';
import { Filterable } from 'app/core/filters/filterable';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServico, OrdemServicoData, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { ExportacaoService } from './../../../../core/services/exportacao.service';
import Enumerable from 'linq';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

@Component({
	selector: 'ordem-servico-lista',
	templateUrl: './ordem-servico-lista.component.html',
	styles: [`
        .list-grid-ordem-servico {
            grid-template-columns: 42px 65px 84px 84px 20px 48px 50px 30px auto 120px auto 40px 120px 50px 100px 10px;
            
            @screen sm {
                grid-template-columns:  48px 80px 92px 92px 36px 36px 56px auto;
            }
        
            @screen md {
                grid-template-columns: 48px 92px 92px 92px 38px 36px 58px 58px auto 120px 58px 10px;
            }
        
            @screen lg {
                grid-template-columns: 42px 65px 84px 84px 20px 48px 50px 30px 40px auto 40px 120px 50px 100px 30px;
            }
        }
    `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})

export class OrdemServicoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	validaCliente: boolean = this._userService.isCustomer;
	validaAbreOS: boolean = this._userService.isOpenOS;
	dataSourceData: OrdemServicoData;
	isLoading: boolean = false;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _cdr: ChangeDetectorRef,
		private _ordemServicoService: OrdemServicoService,
		protected _userService: UserService,
		private _snack: CustomSnackbarService,
		private _stringExtensions: StringExtensions,
		private _exportacaoService: ExportacaoService,
	) {
		super(_userService, 'ordem-servico');
	}

	ngAfterViewInit(): void {
		interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterOrdensServico();
			});

		this.registerEmitters();

		fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.paginator.pageIndex = 0;
			this.obterOrdensServico(text);
		});

		if (this.sort && this.paginator) {
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged();
				this.obterOrdensServico();
			});
		}

		this._cdr.detectChanges();
	}

	async obterOrdensServico(filter: string = '') {
		this.isLoading = true;

		const params: OrdemServicoParameters = {
			pageNumber: this.paginator.pageIndex + 1,
			sortActive: this.filter?.parametros?.sortActive || this.sort.active || 'codOS',
			sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
			pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
			codCliente: this.userSession?.usuario?.codCliente,
			codTipoIntervencaoNotIn: this.validaCliente ? `${TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO},${TipoIntervencaoEnum.ALTERACAO_DE_ENGENHARIA}` : null,
			include: OrdemServicoIncludeEnum.OS_LISTA,
			filter: filter
		};

		if (!this._stringExtensions.isEmptyOrWhiteSpace(filter))
			params.filterType = OrdemServicoFilterEnum.FILTER_GENERIC_TEXT;

		await this._ordemServicoService
			.obterPorParametros({
				...params,
				...this.filter?.parametros
			})
			.subscribe((data) => {
				this.dataSourceData = data;
				this.isLoading = false;
			}, () => {
				this.isLoading = false;
			});
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterOrdensServico();
		})
	}

	loadFilter(): void {
		super.loadFilter();

		if (this.userSession?.usuario?.codFilial && this.userSession?.usuario?.codCliente && this.userSession?.usuario?.codContrato && this.filter) {
			this.filter.parametros.codFiliais = this.userSession?.usuario?.codFilial;
			this.filter.parametros.codClientes = this.userSession?.usuario?.codCliente;
			this.filter.parametros.codContratos = this.userSession?.usuario?.codContrato;
		}
	}


	public async exportarExcel() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.ORDEM_SERVICO,
			entityParameters: {
				...this.filter?.parametros,
				pageSize: 10000,
				include: OrdemServicoIncludeEnum.OS_EXPORTAR,
			}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	public async exportarRAT() {
		this.isLoading = true;

		if (this.dataSourceData.totalCount <= 100) {
			let exportacaoParam: Exportacao = {
				formatoArquivo: ExportacaoFormatoEnum.ZIP,
				tipoArquivo: ExportacaoTipoEnum.ORDEM_SERVICO,
				entityParameters: {
					...this.filter?.parametros,
					pageSize: 10000,
					include: OrdemServicoIncludeEnum.OS_EXPORTAR_ZIP,
				}
			}

			await this._exportacaoService.exportar(FileMime.ZIP, exportacaoParam);
		}
		else {
			this._snack.exibirToast("Seleção do filtro excede o limite de 100 items para exportação", "error");
		}
		this.isLoading = false;
	}

	public async exportarLaudos() {
		this.isLoading = true;

		if (this.dataSourceData.totalCount <= 100) {
			let exportacaoParam: Exportacao = {
				formatoArquivo: ExportacaoFormatoEnum.ZIP,
				tipoArquivo: ExportacaoTipoEnum.LAUDO,
				entityParameters: {
					...this.filter?.parametros,
					pageSize: 10000,
					include: OrdemServicoIncludeEnum.OS_EXPORTAR_ZIP,
				}
			}

			await this._exportacaoService.exportar(FileMime.ZIP, exportacaoParam);
		}
		else {
			this._snack.exibirToast("Seleção do filtro excede o limite de 100 items para exportação", "error");
		}
		this.isLoading = false;
	}

	public async exportarTermos() {
		this.isLoading = true;

		if (this.dataSourceData.totalCount <= 100) {
			let exportacaoParam: Exportacao = {
				formatoArquivo: ExportacaoFormatoEnum.ZIP,
				tipoArquivo: ExportacaoTipoEnum.INSTALACAO,
				entityParameters: {
					...this.filter?.parametros,
					pageSize: 10000,
					include: OrdemServicoIncludeEnum.OS_EXPORTAR_ZIP,
				}
			}

			await this._exportacaoService.exportar(FileMime.ZIP, exportacaoParam);
		}
		else {
			this._snack.exibirToast("Seleção do filtro excede o limite de 100 items para exportação", "error");
		}
		this.isLoading = false;
	}

	paginar() {
		this.onPaginationChanged();
		this.obterOrdensServico();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}

	tooltipSLA(os: OrdemServico) {
		if (os.equipamentoContrato == null || os.equipamentoContrato?.acordoNivelServico == null) return null;

		return os.equipamentoContrato?.acordoNivelServico?.nomeSLA + " - " +
			os.equipamentoContrato?.acordoNivelServico?.descSLA;
	}

	statusSLADescricao(os: OrdemServico) {
		if (os.prazosAtendimento == null) {
			return "---";
		}
		else if (os.statusServico?.codStatusServico == 3 && os.prazosAtendimento?.length > 0) {
			var solucao = Enumerable.from(os.relatoriosAtendimento).orderBy(i => i.codRAT).firstOrDefault()?.dataHoraSolucao || os.dataHoraFechamento;
			if (solucao < os.prazosAtendimento[0]?.dataHoraLimiteAtendimento)
				return "DENTRO";
			return "FORA";
		}
		else if (os.prazosAtendimento?.length > 0) {
			var now = moment();
			var limit = moment(os.prazosAtendimento[0]?.dataHoraLimiteAtendimento);
			if (now < limit)
				return "DENTRO";
			return "FORA";
		}
		return "---";
	}

	statusServicoDescricao(os: OrdemServico) {
		var description = os.statusServico?.nomeStatusServico;
		var usuarioManutencao = os.usuarioManutencao != undefined ? os.usuarioManutencao?.nomeUsuario : '-----';
		var dataManutencao = os.dataManutencao != undefined ? moment(os.dataManutencao).format('DD/MM HH:mm') : '-----';

		if (os.statusServico?.codStatusServico == 7 || os.statusServico?.codStatusServico == 10) {
			var pecas = Enumerable.from(os.relatoriosAtendimento)
				.selectMany(rat => Enumerable.from(rat.relatorioAtendimentoDetalhes)
					.selectMany(d => Enumerable.from(d.relatorioAtendimentoDetalhePecas)
						.select(dp => dp.peca?.codMagnus))).toArray();

			if (pecas?.length > 0)
				description = description + "\nPEÇAS: " + pecas?.join(", ");

			description = description + "\nUsuário Manutenção: " + usuarioManutencao + "\nData Manutenção: " + dataManutencao;
		}
		else if (os.statusServico?.codStatusServico == 6 || os.statusServico?.codStatusServico == 9) {
			description = description + "\nUsuário Manutenção: " + usuarioManutencao + "\nData Manutenção: " + dataManutencao;
		}

		return description;
	}

	tecnicoDescricao(os: OrdemServico) {
		var description = os.tecnico?.nome;
		description += '\n' + 'TRANSFERIDO EM: ';
		description += os.dataHoraTransf ? moment(os.dataHoraTransf).format('DD/MM HH:mm') + '\n' : 'NÃO DISPONÍVEL\n';
		description += 'VISUALIZADO EM: ';
		description += os.dataHoraOSMobileLida ? moment(os.dataHoraOSMobileLida).format('DD/MM HH:mm') : 'NÃO VISUALIZADO';
		return description;
	}

	localDescricao(os: OrdemServico) {
		var description: string = '';

		var nomeLocal = os?.localAtendimento?.nomeLocal?.trim();
		var endereco = os?.localAtendimento?.endereco?.trim();
		var numero = os?.localAtendimento?.numeroEnd?.trim();
		var bairro = os?.localAtendimento?.bairro?.trim();
		var cidade = os?.localAtendimento?.cidade?.nomeCidade?.trim();
		var uf = os?.localAtendimento?.cidade?.unidadeFederativa?.siglaUF?.trim();
		var cep = os?.localAtendimento?.cep?.trim();

		if (nomeLocal)
			description += nomeLocal;

		if (endereco)
			description += nomeLocal ? ', ' + endereco : endereco;

		if (endereco && numero)
			description += ', ' + numero;

		if (endereco && bairro)
			description += ', ' + bairro;

		if (cidade)
			description += ', ' + cidade;

		if (cidade && uf)
			description += ' - ' + uf;

		if (uf && cep)
			description += ', ' + cep + '.';

		return description;
	}
}