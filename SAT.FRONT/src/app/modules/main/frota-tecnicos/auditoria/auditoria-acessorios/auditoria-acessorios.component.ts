import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { AuditoriaVeiculoAcessorio, AuditoriaVeiculoAcessorioParameters } from 'app/core/types/auditoria-veiculo-acessorio.types';
import { AuditoriaVeiculoAcessorioService } from 'app/core/services/auditoria-veiculo-acessorio.service';

@Component({
	selector: 'app-auditoria-acessorios',
	templateUrl: './auditoria-acessorios.component.html',
})
export class AuditoriaAcessoriosComponent implements OnInit {
	codAuditoria: number;
	codAuditoriaVeiculo: number;
	auditoria: Auditoria;
	auditoriaVeiculo: AuditoriaVeiculo;
	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	userSession: UsuarioSessao;
	acessorios: AuditoriaVeiculoAcessorio[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	clienteFilterCtrl: FormControl = new FormControl();
	displayedColumns: string[] = ['nome', 'status', 'justificativa'];

	constructor(
		private _formBuilder: FormBuilder,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _auditoriaService: AuditoriaService,
		private _auditoriaVeiculoAcessoriosService: AuditoriaVeiculoAcessorioService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
		this.inicializarForm();
		this.obterAcessorios();

		this._auditoriaService.obterPorCodigo(this.codAuditoria)
			.pipe(first())
			.subscribe(data => {
				console.log(data);
				
				this.form.patchValue(data);
				this.auditoria = data;
			});
	}

	private async obterAcessorios() {
		const params: AuditoriaVeiculoAcessorioParameters = {
			sortActive: 'nome',
			sortDirection: 'asc',
			pageSize: 100
		}
		const data = await this._auditoriaVeiculoAcessoriosService.obterPorParametros(params).toPromise();
		this.acessorios = data.items;
	}

	private inicializarForm(): void {
		this.form = this._formBuilder.group({
			codAuditoria: [undefined],
			codAuditoriaVeiculo: [undefined],
			nomeUsuario: [undefined],
			nomeFilial: [undefined],
			cpf: [undefined],
			placa: [undefined],
			cnh: [undefined],
			nome: [undefined],
			numCracha: [undefined],
			rg: [undefined],
			cnhCategorias: [undefined],
			finalidadesUso: [undefined],
			cnhValidade: [undefined],
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}