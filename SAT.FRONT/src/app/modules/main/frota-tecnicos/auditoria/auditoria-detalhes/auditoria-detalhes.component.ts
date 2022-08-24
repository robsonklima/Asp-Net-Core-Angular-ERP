import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Usuario, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { UsuarioService } from 'app/core/services/usuario.service';

@Component({
	selector: 'app-auditoria-detalhes',
	templateUrl: './auditoria-detalhes.component.html',
})
export class AuditoriaDetalhesComponent implements OnInit {

	@Output() cod = new EventEmitter();
	
	codAuditoria: number;
    codAuditoriaVeiculo: number;
	auditoria: Auditoria;
    auditoriaVeiculo: AuditoriaVeiculo;
	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	userSession: UsuarioSessao;
	usuarios: Usuario[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	clienteFilterCtrl: FormControl = new FormControl();

	constructor(
		private _formBuilder: FormBuilder,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _auditoriaService: AuditoriaService,
        private _usuarioService: UsuarioService,
		private _snack: CustomSnackbarService,
        private _location: Location,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
        this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
        this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
        this.inicializarForm();
        this.obterUsuarios();

        this._auditoriaService.obterPorCodigo(this.codAuditoria)
				.pipe(first())
				.subscribe(data => {
					this.form.patchValue(data);
					this.auditoria = data;
				});
	}

    private async obterUsuarios()
    {
      const params: UsuarioParameters = {
        sortActive: 'nomeUsuario',
        sortDirection: 'asc',
        indAtivo: statusConst.ATIVO,
        pageSize: 100
      }
      const data = await this._usuarioService.obterPorParametros(params).toPromise();
      this.usuarios = data.items;
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


    salvar(): void
    {
      this.form.disable();
      this.isAddMode ? this.criar() : this.atualizar();
    }
  
    private atualizar(): void
    {
      const form: any = this.form.getRawValue();
  
      let obj = {
        ...this.auditoria,
        ...form,
        ...{
          dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioManut: this.userSession.usuario.codUsuario,
        }
      };
  
      this._usuarioService.atualizar(obj).subscribe(() =>
      {
        this._snack.exibirToast("Usuario atualizada com sucesso!", "success");
        this._location.back();
      }, e =>
      {
        this.form.enable();
      });
    }
  
    private criar(): void
    {
      const form = this.form.getRawValue();
  
      let obj = {
        ...this.auditoria,
        ...form,
        ...{
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioCad: this.userSession.usuario.codUsuario,
        }
      };
  
      this._auditoriaService.criar(obj).subscribe(() =>
      {
        this._snack.exibirToast("Auditoria inserida com sucesso!", "success");
        this._location.back();
      }, e =>
      {
        this.form.enable();
      });
    }

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}