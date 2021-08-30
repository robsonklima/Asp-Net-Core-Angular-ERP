import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { FilialService } from 'app/core/services/filial.service';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { PaisService } from 'app/core/services/pais.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html'
})
export class UsuarioFormComponent implements OnInit, OnDestroy {
  codUsuario: string;
  usuario: Usuario;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _autorizadaService: AutorizadaService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _filialService: FilialService,
    private _paisService: PaisService,
    private _ufService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _location: Location,
    private _userService: UserService,
    private _despesaCartaoCombustivelService: DespesaCartaoCombustivelService,
    private _googleGeolocationService: GoogleGeolocationService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codUsuario = this._route.snapshot.paramMap.get('codUsuario');
    this.isAddMode = !this.codUsuario;
    this.inicializarForm();
    
    if (!this.isAddMode) {
      this._userService.obterPorCodigo(this.codUsuario)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.usuario = data;
      });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codUsuario: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeUsuario: [undefined, Validators.required],
      indAtivo: [undefined],
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();


    let obj = {
      ...this.usuario,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };
    
    // this._userService.atualizar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Usuário ${obj.nomeUsuario} atualizado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.usuario,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    // this._userService.criar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Usuário ${obj.nomeUsuario} adicionado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
