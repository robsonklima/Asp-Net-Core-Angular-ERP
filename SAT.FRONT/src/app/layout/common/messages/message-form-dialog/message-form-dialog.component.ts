import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { MensagemService } from 'app/core/services/mensagem.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { Usuario, UsuarioData, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, map, takeUntil, tap } from 'rxjs/operators';
import { MessagesComponent } from '../messages.component';

@Component({
  selector: 'app-message-form-dialog',
  templateUrl: './message-form-dialog.component.html',
})
export class MessageFormDialogComponent implements OnInit {
  form: FormGroup;
  usuarios: Usuario[] = [];
  usuarioSessao: UsuarioSessao;
  usuarioFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();
  
  constructor(
    private _mensagemService: MensagemService,
    private _usuarioService: UsuarioService,
    public _dialogRef: MatDialogRef<MessagesComponent>,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.criarForm();
    this.registrarEmitters();
    this.usuarios = await (await this.obterUsuarios()).items;
  }

  criarForm() {
    this.form = this._formBuilder.group({
      conteudo: ['', [Validators.required]],
      codUsuarioDestinatario: ['', [Validators.required]]
    });
  }

  salvar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioRemetente: this.usuarioSessao.usuario.codUsuario,
      }
    };

    this._mensagemService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Mensagem enviada com sucesso!", "success");
      this._dialogRef.close();
    });
  }

  private registrarEmitters() {
    this.usuarioFilterCtrl.valueChanges
			.pipe(
				tap(),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					return this.obterUsuarios(query);
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
        const usuarios = (await data).items 

				if (usuarios.length) this.usuarios = usuarios;
			});
  }

  async obterUsuarios(filtro: string = ''): Promise<UsuarioData> {
		let params: UsuarioParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
      codPerfisNotIn: "34,81,87,90,93,97,98",
      ultimoAcessoInicio: moment().subtract(1, 'year').format('YYYY-MM-DD HH:mm:ss')
		};

		return await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
	}

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
