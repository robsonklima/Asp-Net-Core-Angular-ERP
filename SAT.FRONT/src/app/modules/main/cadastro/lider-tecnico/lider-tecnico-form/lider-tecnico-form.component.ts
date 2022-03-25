import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { LiderTecnico } from 'app/core/types/lider-tecnico.types';
import { LiderTecnicoService } from 'app/core/services/lider-tecnico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { Usuario, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-lider-tecnico-form',
  templateUrl: './lider-tecnico-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class LiderTecnicoFormComponent implements OnInit, OnDestroy {
  codLiderTecnico: number;
  isAddMode: boolean;
  form: FormGroup;
  liderTecnico: LiderTecnico;
  userSession: UsuarioSessao;
  public tecnicos: Tecnico[] = [];
  public usuarios: Usuario[] = [];

  usuarioFilterCtrl: FormControl = new FormControl();
  tecnicoFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _liderTecnico: LiderTecnicoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _tecnicoService: TecnicoService,
    private _userService: UserService
  ) { this.userSession = JSON.parse(this._userService.userSession); }

  async ngOnInit() {
    this.codLiderTecnico = +this._route.snapshot.paramMap.get('codLiderTecnico');
    this.isAddMode = !this.codLiderTecnico;

    this.form = this._formBuilder.group({
      codLiderTecnico: [
        {
          value: undefined,
          disabled: true
        },
      ],
      codUsuarioLider: [undefined],
      codTecnico: [undefined]
    });

    this.obterTecnicos();
    this.obterUsuarios();

    if (!this.isAddMode) {
      this._liderTecnico.obterPorCodigo(this.codLiderTecnico)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.liderTecnico = data;
        })
    }

    this.usuarioFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterUsuarios(this.usuarioFilterCtrl.value);
      });

    this.tecnicoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTecnicos(this.tecnicoFilterCtrl.value);
      });
  }

  async obterUsuarios(filtro: string = '') {
    let params: UsuarioParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeUsuario',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._userService
      .obterPorParametros(params)
      .toPromise();

    this.usuarios = data.items;
  }

  async obterTecnicos(filtro: string = '') {
    let params: TecnicoParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      sortActive: 'nome',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._tecnicoService
      .obterPorParametros(params)
      .toPromise();

    this.tecnicos = data.items;
  }

  salvar(): void {

    const form: any = this.form.getRawValue();

    let obj = {
      ...this.liderTecnico,
      ...form
    };

    if (!this.isAddMode) {
      this._liderTecnico.atualizar(obj).subscribe(() => {
        this._snack.exibirToast("Registro atualizado com sucesso!", "success");
        this._location.back();
      })
    } else {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      this._liderTecnico.criar(obj).subscribe(() => {
        this._snack.exibirToast("Registro criado com sucesso!", "success");
        this._location.back();
      })
    }
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
