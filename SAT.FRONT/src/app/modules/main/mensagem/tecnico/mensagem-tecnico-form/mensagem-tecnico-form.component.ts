import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, FormControl, Validators, AbstractControl } from '@angular/forms';
import { tap, map, delay, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { MensagemTecnico } from 'app/core/types/mensagem-tecnico.types';
import { MensagemTecnicoService } from 'app/core/services/mensagem-tecnico.service';
import moment from 'moment';
import { UsuarioService } from 'app/core/services/usuario.service';
import { PerfilEnum } from 'app/core/types/perfil.types';

@Component({
  selector: 'app-mensagem-tecnico-form',
  templateUrl: './mensagem-tecnico-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class MensagemTecnicoFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codMensagemTecnico: number;
  form: FormGroup;
  mensagemTecnico: MensagemTecnico;
  usuarios: Usuario[] = [];
  public isLoading: Boolean = false;
  usuariosFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _usuarioService: UsuarioService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _mensagemTecnicoService: MensagemTecnicoService,
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codMensagemTecnico = +this._route.snapshot.paramMap.get('codMensagemTecnico');
    this.inicializarForm();
    this.registrarEmitters();
    this.obterUsuarios();
  }

  inicializarForm() {
    this.form = this._formBuilder.group({
      codMensagemTecnico: [
        {
          value: undefined,
          disabled: true
        },
      ],
      assunto: [undefined, Validators.required],
      mensagem: [undefined, Validators.required],
      codUsuarios: [undefined, Validators.required],
      indAtivo: [true]
    });
  }

  registrarEmitters() {
    this.usuariosFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterUsuarios(this.usuariosFiltro.value);
      });
  }

  private async obterUsuarios(filtro: string=null) {
    const data = await this._usuarioService.obterPorParametros({ 
      indAtivo: 1, 
      filter: filtro,
      sortActive: 'NomeUsuario', 
      sortDirection: 'ASC', 
      pageSize: 500, 
      codPerfil: PerfilEnum.FILIAL_TECNICO_DE_CAMPO
    }).toPromise();

    this.usuarios = data.items;
  }

  async salvar() {
    this.isLoading = true;
    const form: any = this.form.getRawValue();

    for (const codUsuario of form.codUsuarios) {
      if (!codUsuario) continue;

      let obj = {
        ...form,
        ...{
          codUsuarioDestinatario: codUsuario,
          indAtivo: +form.indAtivo,
          dataHoraCad: moment().format('yyyy-MM-DD HH:MM:ss'),
          codUsuarioCad: this.usuarioSessao.usuario.codUsuario
        }
      };

      await this._mensagemTecnicoService.criar(obj).toPromise();
    }
    
    this._snack.exibirToast("Registro criado com sucesso!", "success");
    this.isLoading = false;
    this._location.back();
  }

  selectAll(select: AbstractControl, values, propertyName) {
    if (select.value[0] == 0 && propertyName != '')
        select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
    else if (select.value[0] == 0 && propertyName == '')
        select.patchValue([...values.map(item => item), 0]);
    else
        select.patchValue([]);
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
