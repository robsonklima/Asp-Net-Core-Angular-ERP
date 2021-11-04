import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';

@Component({
  selector: 'app-ponto-periodo-form',
  templateUrl: './ponto-periodo-form.component.html'
})
export class PontoPeriodoFormComponent implements OnInit, OnDestroy {
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _pontoPeriodoSvc: PontoPeriodoService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.isAddMode = !this.codPontoPeriodo;
    
    this.form = this._formBuilder.group({
      codPontoPeriodo: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      dataInicio: [undefined, Validators.required],
      dataFim: [undefined, Validators.required],
      codPontoPeriodoStatus: [undefined, Validators.required],
      codPontoPeriodoModoAprovacao: [undefined, Validators.required],
      codPontoPeriodoIntervaloAcessoData: [undefined, Validators.required]
    });
    
    if (!this.isAddMode) {
      this._pontoPeriodoSvc.obterPorCodigo(this.codPontoPeriodo)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.pontoPeriodo = data;
          console.log(data);
          
        });
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.pontoPeriodo,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario
      }
    };
    
    // this._equipamentoService.atualizar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Equipamento ${obj.nomeEquip} atualizado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.pontoPeriodo,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario
      }
    };

    // this._equipamentoService.criar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Equipamento ${obj.nomeEquip} adicionado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
