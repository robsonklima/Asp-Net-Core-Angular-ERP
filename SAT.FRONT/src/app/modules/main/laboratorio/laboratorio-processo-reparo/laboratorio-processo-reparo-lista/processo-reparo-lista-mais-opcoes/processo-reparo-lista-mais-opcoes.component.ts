import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORStatusService } from 'app/core/services/or-status.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { ORItem } from 'app/core/types/or-item.types';
import { ORStatus, ORStatusData, ORStatusParameters } from 'app/core/types/or-status.types';
import { statusConst } from 'app/core/types/status-types';
import { Usuario, UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil } from 'rxjs/operators';
import { LaboratorioProcessoReparoListaComponent } from '../laboratorio-processo-reparo-lista.component';

@Component({
  selector: 'app-processo-reparo-lista-mais-opcoes',
  templateUrl: './processo-reparo-lista-mais-opcoes.component.html'
})
export class ProcessoReparoListaMaisOpcoesComponent implements OnInit {
  itens: ORItem[] = [];
  usuarios: Usuario[] = [];
  status: ORStatus[] = [];
  tecnicoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();
  form: FormGroup;
  loading: boolean = true;
  usuarioSessao: UserSession;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<LaboratorioProcessoReparoListaComponent>,
    private _orStatusService: ORStatusService,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _usuarioService: UsuarioService,
    private _userService: UserService,
    private _formBuilder: FormBuilder
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
    if (data) this.itens = data.itens;
  }

  async ngOnInit() {
    this.criarForm();
    this.registrarEmitters();
    this.usuarios = (await this.obterUsuariosTecnicos()).items;
    this.status = (await this.obterStatus()).items;
    this.loading = false;
  }

  private criarForm() {
    this.form = this._formBuilder.group({
      codTecnico: [undefined],
      codStatus: [undefined],
    });
  }

  private registrarEmitters() {
    this.tecnicoFilterCtrl.valueChanges.pipe(
			filter(query => !!query),
			debounceTime(700),
			map(async query => {
				return query;
			}),
			delay(500),
			takeUntil(this._onDestroy)
		)
		.subscribe(async promisse => {
			promisse.then(async query => {
				this.usuarios = (await this.obterUsuariosTecnicos(query)).items;
			});
		});
  }

  async obterUsuariosTecnicos(filter: string=null): Promise<UsuarioData> {
		let params: UsuarioParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			codPerfis: "63,61",
      filter: filter
		};

		return await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
	}

  async obterStatus(): Promise<ORStatusData> {
		let params: ORStatusParameters = {
			sortActive: 'descStatus',
			sortDirection: 'asc'
		};

		return await this._orStatusService
			.obterPorParametros(params)
			.toPromise();
	}

  async salvar() {
    const form = this.form.getRawValue();

    for (const item of this.itens) {
      this._orItemService
        .atualizar({
          ...item,
          ...{
            codTecnico: form.codTecnico || item.codTecnico,
            codStatus: form.codStatus || item.codStatus
          }
        })
        .subscribe(() => {
          this._snack.exibirToast('Registros atualizados com sucesso', 'success');
          this.dialogRef.close(true);
        }, () => {
          this._snack.exibirToast('Erro ao atualizar os registros', 'error');
          this.dialogRef.close(true);
        });
    }
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
