import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
	selector: 'app-suporte-stn-bloquear-os',
	templateUrl: './suporte-stn-bloquear-os.component.html',
	styles: [`
    .list-grid-pesquisa {
      grid-template-columns: 72px 90px 200px 200px 150px auto 250px;
    }  
  `],
	encapsulation: ViewEncapsulation.None,
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class SuporteStnBloquearOSComponent implements OnInit, OnDestroy {
	@ViewChild(MatSort) private sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	form: FormGroup;
	dataSourceData: OrdemServicoData;
	isLoading: boolean = false;
	userSession: UserSession;
	private _unsubscribeAll: Subject<any> = new Subject<any>();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _osSvc: OrdemServicoService,
		private _userService: UserService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngOnInit(): void {
		this.criarForm();

		if (this.sort && this.paginator) {
			this.sort.sortChange.subscribe(() => {
				this.paginator.pageIndex = 0;
				this.pesquisar();
			});
		}
		this._cdr.detectChanges();
	}

	criarForm() {
		this.form = this._formBuilder.group({
			codOS: [''],
		});
	}

	pesquisar() {
		const form = this.form.getRawValue();
		const isEmpty = Object.values(form).every(x => x === null || x === '' || x === undefined);

		if (isEmpty) {
			this._snack.exibirToast('Favor informar sua pesquisa', 'warning');
			return;
		}
		this.obterChamados();
	}

	monitorarDigitacaoForm(e) {
		if (e.keyCode === 13) {
			this.pesquisar()
		}
	}

	private obterChamados() {
		const form = this.form.getRawValue();
		this.isLoading = true;

		this._osSvc.obterPorParametros({
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'codOS',
			sortDirection: this.sort.direction || 'desc',
			pageSize: this.paginator?.pageSize,
			codOS: form.codOS
		}).subscribe((data: OrdemServicoData) => {
			if (data.items.length === 0) {
				this._snack.exibirToast('Nenhum chamado encontrado', 'error');
			}

			this.dataSourceData = data;
			this.isLoading = false;
			this._cdr.detectChanges();
		}, () => {
			this.isLoading = false;
		});
	}

	public async bloquearDesbloquearChamado(os: OrdemServico) {
		this.isLoading = true;

		os.indBloqueioReincidencia = os.indBloqueioReincidencia ? 0 : 1;
		os.codUsuarioManut = this.userSession.usuario.codUsuario;
		os.codUsuarioManutencao = this.userSession.usuario.codUsuario;
		os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

		await this._osSvc.atualizar(os).toPromise();
		await this.obterChamados();

		this.isLoading = false;
	}

	paginar() {
		this.pesquisar();
	}

	ngOnDestroy(): void {
		this._unsubscribeAll.next();
		this._unsubscribeAll.complete();
	}
}
