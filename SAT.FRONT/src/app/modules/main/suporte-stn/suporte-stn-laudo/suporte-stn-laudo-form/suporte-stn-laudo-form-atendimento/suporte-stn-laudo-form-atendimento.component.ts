import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
import { LaudoStatus, LaudoStatusParameters } from 'app/core/types/laudo-status.types';
import { LaudoStatusService } from 'app/core/services/laudo-status.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LaudoSituacao } from 'app/core/types/laudo-situacao.types';
import { LaudoSituacaoService } from 'app/core/services/laudo-situacao.service';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

@Component({
	selector: 'app-suporte-stn-laudo-form-atendimento',
	templateUrl: './suporte-stn-laudo-form-atendimento.component.html'
})
export class SuporteStnLaudoFormAtendimentoComponent implements OnInit, OnDestroy {

	codLaudo: number;
	laudo: Laudo;
	status: LaudoStatus[] = [];
	situacao: LaudoSituacao;
	isLoading: boolean;
	userSession: UsuarioSessao;
	form: FormGroup;
	form2: FormGroup;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _route: ActivatedRoute,
		private _formBuilder: FormBuilder,
		private _userService: UserService,
		private _laudoService: LaudoService,
		private _laudoStatusService: LaudoStatusService,
		private _laudoSituacaoService: LaudoSituacaoService,
		private _snack: CustomSnackbarService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');
		this.laudo = await this._laudoService.obterPorCodigo(this.codLaudo).toPromise();

		this.obterLaudoStatus();
		this.obterLaudoSituacao();
		this.criarForm();
	}


	criarForm() {
		this.form = this._formBuilder.group({
			relatoCliente: [undefined],
			conclusao: [undefined],
			codLaudoStatus: [this.laudo.codLaudoStatus],
			tensaoComCarga: [undefined],
			tensaoSemCarga: [undefined],
			tensaoTerraENeutro: [undefined],
			temperatura: [undefined],
			indRedeEstabilizada: [undefined],
			indPossuiNobreak: [undefined],
			indPossuiArCond: [undefined],
		});
		this.form2 = this._formBuilder.group({
			causa: [undefined],
			acao: [undefined],
		});
	}

	async obterLaudoStatus(filtro: string = '') {
		let params: LaudoStatusParameters = {
			codLaudoStatus: this.laudo.codLaudoStatus,
			filter: filtro,
			sortActive: 'nomeStatus',
			sortDirection: 'asc',
		};

		const data = await this._laudoStatusService.obterPorParametros(params).toPromise();
		this.status = data.items;
	}

	async obterLaudoSituacao() {
		this.situacao = (await this._laudoSituacaoService.obterPorParametros({ codLaudo: this.laudo.codLaudo }).toPromise()).items.shift();
	}

	private atualizarLaudo() {
		this.form.disable();
		const form: any = this.form.getRawValue();
		let obj = {
			...this.laudo,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		this._laudoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Laudo atualizada com sucesso!", "success");
		});
	}

	private atualizarSituacao() {
		this.form.disable();
		const form: any = this.form2.getRawValue();
		let obj = {
			...this.situacao,
			...form
		};

		this._laudoSituacaoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Situação atualizada com sucesso!", "success");
		});
	}

	salvar() {
		this.atualizarLaudo();
		this.atualizarSituacao();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}